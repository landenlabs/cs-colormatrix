@echo off

@rem may need to install .net SDK 
@rem winget install Microsoft.DotNet.SDK.10
@rem winget install Microsoft.DotNet.DesktopRuntime.10


@rem Optional define bindir and msbuild if not current set correctly. 
call ..\dev-setup.bat

set prog=ColorMatrix
set reldeb=Debug

cd %prog% 
 
@echo ---- Clean Release %prog% 
lldu -sum obj bin 
rmdir /s obj  2> nul
rmdir /s bin  2> nul
@rem "%msbuild%" "%prog%.sln"  -t:Clean

@echo.
@echo ---- Build %reldeb% %prog% 
@rem with the .NET CLI wrapper (recommended): 
@rem dotnet msbuild -t:Publish -p:Configuration=Release -p:RuntimeIdentifier=win-x64 -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true
"%msbuild%" "%prog%.sln" -m -p:Configuration="%reldeb%";RuntimeIdentifier=win-x64 -verbosity:minimal  -detailedSummary:True /restore
set blddir=bin\%reldeb%\net10.0-windows\win-x64
set exe=%blddir%\%prog%.exe

@echo.
@echo ---- Build done 
if not exist "%exe%" (
   echo Failed to build "%exe%"
   goto _end
)
 
@echo ---- Cleanup 
lr -p  %bindir%\%prog%.*

@echo ---- Copy "%exe%" to %bindir%
copy %blddir%\%prog%.exe %bindir%\ 
copy %blddir%\%prog%.dll %bindir%\
copy %blddir%\%prog%.runtimeconfig.json  %bindir%\
%dir% "%exe%" %bindir%\%prog%.exe

@rem play happy tone
rundll32.exe cmdext.dll,MessageBeepStub
rundll32 user32.dll,MessageBeep
 
:_end
cd ..
 