# ============================================================
# GitHub 推送脚本
# 项目: AiApplication -> bochen-jinxi/AiApplication
# 使用方法:
#   1. 在 GitHub 创建 PAT: Settings -> Developer settings ->
#      Personal access tokens -> Tokens (classic) -> Generate new token
#      勾选权限: repo (Full control of private repositories)
#   2. 将 PAT 填入下方 $PAT 变量
#   3. 运行: powershell -ExecutionPolicy Bypass -File push-to-github.ps1
# ============================================================

# ====== 请在此处填入您的 GitHub Personal Access Token ======
$PAT = "在此粘贴您的PAT"
# ============================================================

$RepoName = "AiApplication"
$Owner    = "bochen-jinxi"
$Description = "AiApplication - ASP.NET Core 3.1 AI 应用 (分层架构)"

# 校验 PAT
if ($PAT -eq "在此粘贴您的PAT" -or [string]::IsNullOrWhiteSpace($PAT)) {
    Write-Host "[ERROR] 请先在脚本中填入您的 GitHub PAT" -ForegroundColor Red
    exit 1
}

Write-Host "步骤 1: 通过 GitHub API 创建仓库 $Owner/$RepoName ..." -ForegroundColor Cyan

$headers = @{
    "Authorization" = "token $PAT"
    "Accept"        = "application/vnd.github+json"
    "User-Agent"    = "CodeArts-Agent"
}
$body = @{
    name        = $RepoName
    description = $Description
    private     = $false
    auto_init   = $false
} | ConvertTo-Json

try {
    $response = Invoke-RestMethod -Uri "https://api.github.com/user/repos" -Method Post -Headers $headers -Body $body -ContentType "application/json"
    Write-Host "[OK] 仓库创建成功: $($response.html_url)" -ForegroundColor Green
} catch {
    if ($_.Exception.Response.StatusCode.value__ -eq 422) {
        Write-Host "[WARN] 仓库可能已存在，继续推送..." -ForegroundColor Yellow
    } else {
        Write-Host "[ERROR] 创建仓库失败: $($_.Exception.Message)" -ForegroundColor Red
        exit 1
    }
}

Write-Host "步骤 2: 配置远程仓库 origin ..." -ForegroundColor Cyan
cd c:\AiApplication
git remote remove origin 2>$null
git remote add origin "https://${PAT}@github.com/${Owner}/${RepoName}.git"
Write-Host "[OK] 远程已配置" -ForegroundColor Green

Write-Host "步骤 3: 推送 master 分支 ..." -ForegroundColor Cyan
git push -u origin master 2>&1 | ForEach-Object { Write-Host $_ }
if ($LASTEXITCODE -eq 0) {
    Write-Host "[OK] 推送成功!" -ForegroundColor Green
} else {
    Write-Host "[ERROR] 推送失败" -ForegroundColor Red
    exit 1
}

Write-Host "步骤 4: 清理凭据痕迹（移除 URL 中的 PAT）..." -ForegroundColor Cyan
git remote set-url origin "https://github.com/${Owner}/${RepoName}.git"
Write-Host "[OK] 已清理 URL 中的 PAT" -ForegroundColor Green

Write-Host "步骤 5: 验证 ..." -ForegroundColor Cyan
git remote -v
git log --oneline -5
Write-Host ""
Write-Host "================================================" -ForegroundColor Green
Write-Host "推送完成! 仓库地址: https://github.com/$Owner/$RepoName" -ForegroundColor Green
Write-Host "================================================" -ForegroundColor Green
