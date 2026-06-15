<#
.SYNOPSIS
    TagMan v0.1 - Gerenciador de Tags Git.
.DESCRIPTION
    Script para gerenciar tags Git locais e remotas interativamente ou via linha de comando.
.PARAMETER Action
    A ação a ser executada diretamente (Create, Delete, Push, DeleteRemote, List).
.PARAMETER TagName
    O nome da tag para a ação especificada.
.PARAMETER Message
    A mensagem anotada para a criação de uma nova tag.
.EXAMPLE
    .\TagMan.ps1
    (Inicia o modo interativo)
.EXAMPLE
    .\TagMan.ps1 -Action Create -TagName "v1.0.0" -Message "Release inicial"
    (Cria uma tag inline)
#>

param (
    [ValidateSet('Create', 'Delete', 'Push', 'DeleteRemote', 'List')]
    [string]$Action,

    [string]$TagName,

    [string]$Message
)

# --- ALIASES PARA EXECUÇÃO INLINE ---
# Nota: Aliases de funções dentro do script funcionam se passados por parâmetros mapeados.
# Para facilitar o uso inline avançado, mapeamos as funções principais abaixo.

function Show-Header {
    Clear-Host
    Write-Host @"
****************************************************
* * * * * *
* TagMan  * 
*  v0.1   *
* * * * * *      
           ___________ 
          /___   ____/ _____   ______
             /  /     /  __ \/  __   /
            /  /     /  / |   /  /  /
           /  /     /  /  /__/  /  /
          /__/     /__/        /__/

****************************************************
"@ -ForegroundColor Cyan
}

function Test-GitRepository {
    if (-not (git rev-parse --is-inside-work-tree 2>$null)) {
        Write-Error "Erro: Este diretorio nao e um repositorio Git valido."
        Exit 1
    }
}

function New-GitTag {
    [CmdletBinding()]
    param([string]$Name, [string]$Msg)
    process {
        if (-not $Name) { $Name = Read-Host "Digite o nome da tag (ex: v1.0.0)" }
        if (-not $Name) { Write-Warning "Nome da tag nao pode ser vazio."; return }

        if (-not $Msg) { $Msg = Read-Host "Digite a mensagem da tag (deixe vazio para tag leve)" }

        if ($Msg) {
            git tag -a $Name -m $Msg
        } else {
            git tag $Name
        }
    }
}
Set-Alias -Name 'ctag' -Value New-GitTag

function Remove-GitTagLocal {
    [CmdletBinding()]
    param([string]$Name)
    process {
        if (-not $Name) { $Name = Read-Host "Digite o nome da tag local a ser excluida" }
        if (-not $Name) { return }

        git tag -d $Name
    }
}
Set-Alias -Name 'rtag' -Value Remove-GitTagLocal

function Push-GitTag {
    [CmdletBinding()]
    param([string]$Name)
    process {
        if (-not $Name) { $Name = Read-Host "Digite o nome da tag para enviar (ou 'all' para todas)" }
        if (-not $Name) { return }

        if ($Name -eq "all") {
            git push origin --tags
        } else {
            git push origin $Name
        }
    }
}
Set-Alias -Name 'ptag' -Value Push-GitTag

function Remove-GitTagRemote {
    [CmdletBinding()]
    param([string]$Name)
    process {
        if (-not $Name) { $Name = Read-Host "Digite o nome da tag remota a ser excluida" }
        if (-not $Name) { return }

        git push origin --delete $Name
    }
}
Set-Alias -Name 'drtag' -Value Remove-GitTagRemote

function Get-GitTagsSummary {
    Write-Host "`n--- Tags Locais ---" -ForegroundColor Yellow
    git tag
    Write-Host "`n--- Tags no Remoto (Origin) ---" -ForegroundColor Yellow
    git ls-remote --tags origin | ForEach-Object { $_ -replace '.*refs/tags/', '' }
    Write-Host ""
}
Set-Alias -Name 'ltag' -Value Get-GitTagsSummary

<# ------------
## --- MAIN ---
## --------- #> 
Test-GitRepository

if ($PSBoundParameters.ContainsKey('Action')) {
    switch ($Action) {
        'Create'       { New-GitTag -Name $TagName -Msg $Message }
        'Delete'       { Remove-GitTagLocal -Name $TagName }
        'Push'         { Push-GitTag -Name $TagName }
        'DeleteRemote' { Remove-GitTagRemote -Name $TagName }
        'List'         { Get-GitTagsSummary }
    }
    Exit 0
}

$loop = $true
while ($loop) {
    Show-Header
    Get-GitTagsSummary

    Write-Host "Selecione uma operacao:" -ForegroundColor Green
    Write-Host "1) Criar Tag Local (ctag)"
    Write-Host "2) Excluir Tag Local (rtag)"
    Write-Host "3) Enviar Tag para o Remoto (ptag)"
    Write-Host "4) Excluir Tag do Remoto (drtag)"
    Write-Host "5) Atualizar Lista (ltag)"
    Write-Host "6) Sair"
    
    $opcao = Read-Host "`nOpcao"

    switch ($opcao) {
        '1' { New-GitTag }
        '2' { Remove-GitTagLocal }
        '3' { Push-GitTag }
        '4' { Remove-GitTagRemote }
        '5' { # Apenas recarrega o loop 
            continue 
        }
        '6' { 
            $loop = $false
            Write-Host "Saindo do TagMan. Ate logo!" -ForegroundColor Cyan
            continue
        }
        Default { 
            Write-Warning "Opcao invalida! Pressione qualquer tecla para tentar novamente..."
            [void]$Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
            continue
        }
    }

    if ($loop) {
        Write-Host ""
        $desejaContinuar = Read-Host "Deseja realizar outra operacao? (S/N)"
        if ($desejaContinuar -notmatch '^[sS](im)?$') {
            $loop = $false
            Write-Host "Saindo do TagMan. Ate logo!" -ForegroundColor Cyan
        }
    }
}