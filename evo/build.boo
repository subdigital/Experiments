import System
import System.IO

build_dir = Path.Combine(Environment.CurrentDirectory, "build\\")
mspec = "tools/mspec/Machine.Specifications.ConsoleRunner.exe"
configuration = "Debug"
test_assembly = "src\\evo.Tests\\bin\\${configuration}\\evo.Tests.dll"
ver = "0.1"

target debug:
	print build_dir
	
target init:
	rmdir(build_dir)
	
target release:
	configuration = "Release"
	test_assembly = test_assembly.Replace("Debug", "Release")

target compile, (init):
	msbuild("evo.sln", {@configuration:configuration, @OutDir:build_dir})
	
target foo:
	exec("cd ${build_dir}")

target test, (compile):
	exec("cd ${build_dir}")	  
	exec(mspec, test_assembly)
	exec("cd ..")
	
target report, (compile):		
	exec(mspec, "--html ${build_dir}\\Specs.html ${test_assembly}", {@WorkingDir:build_dir})
	exec("start ${build_dir}\\Specs.html")
	
target package, (release, compile, test):	  	
	zip "${build_dir}", "${build_dir}\\evo-${configuration}-${version}.zip"

target default, (test):
	pass