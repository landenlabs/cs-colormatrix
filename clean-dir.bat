@echo off

set prog=ColorMatrix

call ..\dev-setup.bat

cd %prog% 
 
@echo ---- Clean Release %prog% 
"%msbuild%" "%prog%.sln" /t:Clean
lldu -sum obj bin 
rmdir /s obj  2> nul
rmdir /s bin  2> nul

cd ..