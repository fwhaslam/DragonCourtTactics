REM 
REM this file needs to 'save as' US-ASCII so it will function, default of UTF-8 will make it un-runnnable.
REM
echo     Trying to copy DLL into Unity

set SolutionDir=%1
set TargetDir=%2

REM copy build into Unity DLL folder
IF exist %SolutionDir%Assets\_DLLs\ (
    echo     Copying DLL 
    echo     from %TargetDir%; 
    echo     into %SolutionDir%Assets\DLLs; 
    copy %TargetDir%\*.dll %SolutionDir%Assets\_DLLs\.
) ELSE ( 
    echo     No Assets, DLL not copied 
)
