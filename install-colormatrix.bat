@echo off

@rem Optional define bindir and msbuild if not current set correctly. 
call ..\dev-setup.bat

set prog=ColorMatrix
set reldeb=Debug

cd %prog% 
 
@echo ---- Clean Release %prog% 
lldu -sum obj bin 
rmdir /s obj  2> nul
rmdir /s bin  2> nul
@rem %msbuild% %prog%.sln  -t:Clean

@echo.
@echo ---- Build %reldeb% %prog% 
%msbuild% %prog%.sln -m -p:Configuration="%reldeb%";Platform="Any CPU" -verbosity:minimal  -detailedSummary:True

@echo.
@echo ---- Build done 
if not exist "bin\%reldeb%\%prog%.exe" (
   echo Failed to build bin\%reldeb%\%prog%.exe
   goto _end
)
 
@echo ---- Copy %reldeb% to %bindir%
copy  bin\%reldeb%\%prog%.exe %bindir%\%prog%.exe
%dir%   bin\%reldeb%\%prog%.exe %bindir%\%prog%.exe

@rem play happy tone
rundll32.exe cmdext.dll,MessageBeepStub
rundll32 user32.dll,MessageBeep
 
:_end
cd ..
 