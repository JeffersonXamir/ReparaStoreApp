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
SetupIconFile=..\ReparaStoreApp.WPF\Assets\Icons\ReparaStoreAppIcon.ico

[Files]
Source: "..\ReparaStoreApp.WPF\bin\Release\net8.0-windows\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs
Source: "..\ReparaStoreApp.WPF\appsettings.json"; DestDir: "{app}"; Flags: ignoreversion onlyifdoesntexist
Source: "..\ReparaStoreApp.WPF\Assets\Icons\ReparaStoreAppIcon.ico"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
; Icono en el menú de inicio
Name: "{group}\ReparaStore"; Filename: "{app}\ReparaStoreApp.WPF.exe"; IconFilename: "{app}\ReparaStoreAppIcon.ico"

; Icono en el escritorio
Name: "{commondesktop}\ReparaStore"; Filename: "{app}\ReparaStoreApp.WPF.exe"; IconFilename: "{app}\ReparaStoreAppIcon.ico"

; Icono para desinstalar
Name: "{group}\Desinstalar ReparaStore"; Filename: "{uninstallexe}"; IconFilename: "{app}\ReparaStoreAppIcon.ico"

[Run]
Filename: "{app}\ReparaStoreApp.WPF.exe"; Parameters: "--migrate"; Description: "Configurar base de datos"; Flags: postinstall nowait skipifsilent runascurrentuser
Filename: "{app}\ReparaStoreApp.WPF.exe"; Description: "Iniciar aplicación"; Flags: postinstall nowait skipifsilent

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
  if (CurStep = ssPostInstall) and (AppSettingsJson <> '') then
  begin
    SaveStringToFile(ExpandConstant('{app}\appsettings.json'), AppSettingsJson, False);
  end;
end;