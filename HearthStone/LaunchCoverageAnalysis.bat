@ECHO OFF

REM OpenCover-MSTest.bat

REM Run opencover against MSTest tests in your test project and show report of code coverage

REM Derivative work based on work by: 
REM  Shaun Wilde - https://www.nuget.org/packages/OpenCover/
REM  Daniel Palme - https://www.nuget.org/packages/ReportGenerator/
REM  Allen Conway - 
REM   http://www.allenconway.net/2015/06/using-opencover-and-reportgenerator-to.html
REM  Andrew Newton - 
REM   http://www.nootn.com.au/2014/01/code-coverage-with-opencover-example.html#.VxiNn_krLDc


SET DllContainingTests=%~dp0HearthStone.Library.Test\bin\Debug\HearthStone.Library.Test.dll


REM *** IMPORTANT - Change DllContainingTests variable (above) to point to the DLL 
REM ***             in your solution containing your NUnit tests
REM ***
REM ***             You may also want to change the include/exclude filters 
REM ***             (below) for OpenCover
REM ***
REM ***             This batch file should dbe placed in the root folder of your solution

REM *** Before being able to use this to generate coverage reports you will 
REM *** need the following NuGet packages
REM PM> Install-Package OpenCover
REM PM> Install-Package ReportGenerator
REM

REM *** MSTest Test Runner (VS2013, will need to change 12.0 to 14.0 for VS2015)
SET TestRunnerExe=%PROGRAMFILES(X86)%\Microsoft Visual Studio 14.0\Common7\IDE\MSTest.exe

REM Get OpenCover Executable (done this way so we dont have to change 
REM the code when the version number changes)
for /R "%~dp0packages" %%a in (*) do if /I "%%~nxa"=="OpenCover.Console.exe" SET OpenCoverExe=%%~dpnxa

REM Get Report Generator (done this way so we dont have to change the code 
REM when the version number changes)
for /R "%~dp0packages" %%a in (*) do if /I "%%~nxa"=="ReportGenerator.exe" SET ReportGeneratorExe=%%~dpnxa

REM Create a 'GeneratedReports' folder if it does not exist
if not exist "%~dp0GeneratedReports" mkdir "%~dp0GeneratedReports"

REM Run the tests against the targeted output
call :RunOpenCoverUnitTestMetrics

REM Generate the report output based on the test results
if %errorlevel% equ 0 ( 
 call :RunReportGeneratorOutput 
)

REM Launch the report
if %errorlevel% equ 0 ( 
 call :RunLaunchReport 
)
exit /b %errorlevel%

:RunOpenCoverUnitTestMetrics 
REM *** Change the filter to include/exclude parts of the solution you want to 
REM *** check for test coverage
"%OpenCoverExe%" ^
 -target:"%TestRunnerExe%" ^
 -targetargs:"/noisolation /testcontainer:\"%DllContainingTests%\"" ^
 -filter:"+[*]* -[*]HearthStone.Library.CommunicationInfrastructure.* -[*]HearthStone.Library.EndPoint -[*]HearthStone.Library.GameManager -[*]HearthStone.Library.LogService -[*]HearthStone.Library.Player -[*]HearthStone.Library.WaitingPlayerCounter" ^
 -mergebyhash ^
 -skipautoprops ^
 -register:user ^
 -output:"%~dp0GeneratedReports\CoverageReport.xml"
exit /b %errorlevel%

:RunReportGeneratorOutput
"%ReportGeneratorExe%" ^
 -reports:"%~dp0\GeneratedReports\CoverageReport.xml" ^
 -targetdir:"%~dp0\GeneratedReports\ReportGenerator Output"
exit /b %errorlevel%

:RunLaunchReport
start "report" "%~dp0\GeneratedReports\ReportGenerator Output\index.htm"
exit /b %errorlevel%