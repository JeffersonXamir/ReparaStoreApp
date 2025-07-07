; Script de instalación para ReparaStore
#define AppVersion "1.0.1"

[Setup]
AppName=ReparaStore
AppVersion={#AppVersion} Elaborado por la Universidad de Guayaquil
DefaultDirName={autopf}\ReparaStore
DefaultGroupName=ReparaStore
OutputBaseFilename=ReparaStoreSetup
Compression=lzma
SolidCompression=yes
PrivilegesRequired=admin
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64
; Agregado para evitar advertencias
WizardStyle=modern

[Files]
Source: "ReparaStoreApp.WPF\bin\Release\net8.0-windows\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs
Source: "ReparaStoreApp.WPF\appsettings.json"; DestDir: "{app}"; Flags: ignoreversion onlyifdoesntexist

[Icons]
Name: "{group}\ReparaStore"; Filename: "{app}\ReparaStoreApp.WPF.exe"
Name: "{commondesktop}\ReparaStore"; Filename: "{app}\ReparaStoreApp.WPF.exe"

[Run]
Filename: "{app}\ReparaStoreApp.WPF.exe"; Parameters: "--migrate"; Description: "{cm:ConfigDatabase}"; Flags: postinstall nowait skipifsilent runascurrentuser
Filename: "{app}\ReparaStoreApp.WPF.exe"; Description: "{cm:LaunchProgram}"; Flags: postinstall nowait skipifsilent

[CustomMessages]
ConfigDatabase=Configurar base de datos
LaunchProgram=Iniciar ReparaStore

[Code]
var
  DatabasePage: TInputQueryWizardPage;
  AppSettingsJson: string;

procedure InitializeWizard;
begin
  DatabasePage := CreateInputQueryPage(wpWelcome,
    'Configuración de Base de Datos',
    'Ingrese los parámetros de conexión a MariaDB',
    'Estos valores se guardarán en la configuración de la aplicación');
    
  DatabasePage.Add('Servidor:', False);
  DatabasePage.Add('Base de datos:', False);
  DatabasePage.Add('Usuario:', False);
  DatabasePage.Add('Contraseña:', True);
  DatabasePage.Add('Puerto:', False);
  
  // Valores por defecto
  DatabasePage.Values[0] := 'localhost';
  DatabasePage.Values[1] := 'ReparaStoreDb';
  DatabasePage.Values[2] := 'root';
  DatabasePage.Values[3] := '';
  DatabasePage.Values[4] := '3306';
end;

function NextButtonClick(CurPageID: Integer): Boolean;
begin
  if CurPageID = DatabasePage.ID then
  begin
    // Preparamos el JSON pero NO lo guardamos aún
    AppSettingsJson := 
    '{' + #13#10 +
    '  "ConnectionStrings": {' + #13#10 +
    '    "MariaDB": "Server=' + DatabasePage.Values[0] + 
                  ';Database=' + DatabasePage.Values[1] + 
                  ';User=' + DatabasePage.Values[2] + 
                  ';Password=' + DatabasePage.Values[3] + 
                  ';Port=' + DatabasePage.Values[4] + ';"' + #13#10 +
    '  },' + #13#10 +
    '  "JwtSettings": {' + #13#10 +
    '    "SecretKey": "c39Yf9SeUGGvXS9vmEM3bBqEx5DNmabV",' + #13#10 +
    '    "Issuer": "ReparaStoreApp",' + #13#10 +
    '    "Audience": "ReparaStoreUsers",' + #13#10 +
    '    "ExpirationMinutes": 120' + #13#10 +
    '  }' + #13#10 +
    '}';
  end;
  Result := True;
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  // Esperamos hasta que la instalación esté en progreso
  if (CurStep = ssPostInstall) and (AppSettingsJson <> '') then
  begin
    // Ahora {app} está disponible y podemos guardar el archivo
    SaveStringToFile(ExpandConstant('{app}\appsettings.json'), AppSettingsJson, False);
  end;
end;