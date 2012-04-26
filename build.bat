@echo off

rem Needed to update variable in loop
setlocal enabledelayedexpansion

echo Getting list of files

for %%i in (.\bin\AnyCPU\*.dll) do (set files=!files!%%~i )

echo Merging files

tools\ILMerge\ILMerge.exe /out:build\CHAOS.Statistics.dll /lib:C:\Windows\Microsoft.NET\Framework64\v4.0.30319 /targetplatform:v4,C:\Windows\Microsoft.NET\Framework64\v4.0.30319 /lib:lib\ %files%

echo Done

pause