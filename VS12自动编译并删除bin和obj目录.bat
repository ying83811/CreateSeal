'create by xihumeijing

@echo off
set FolderName=%cd%
set dis=%~d0
for /f "delims=\" %%a in ('dir /b /a-d /o-d "%FolderName%\*.sln"') do (
  set names=%%a
)
cd C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319
C:
@echo on
MSBuild.exe /p:VisualStudioVersion=12.0 /m "%FolderName%\%names%"
@echo off
color 06
echo ���������ʼɾ��obj��binĿ¼
color 07
pause>nul
@echo on

@echo off
cd %FolderName%
%dis%
@echo on
for /f "tokens=*" %%a in ('dir obj /b /ad /s ^|sort') do rd "%%a" /s/q
for /f "tokens=*" %%a in ('dir bin /b /ad /s ^|sort') do rd "%%a" /s/q
del *.sln.cache
del /ah *.v12.suo
del *.GhostDoc.xml
@echo off
echo ��������˳�
pause>nul