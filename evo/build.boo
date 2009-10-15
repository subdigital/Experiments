import System
import System.IO

build_dir = Path.Combine(Environment.CurrentDirectory, "build\\")
mspec = "tools/mspec/Machine.Specifications.ConsoleRunner.exe"
test_assembly = build_dir + "evo.Tests.dll"

target debug:
	print build_dir

target init:
	rmdir(build_dir)
	mkdir(build_dir)

target compile, (init):
	msbuild("evo.sln", {@OutDir:build_dir})

target test, (compile):
	exec(mspec, test_assembly)
	
target testReport, (compile):
	exec(mspec, "--html ${build_dir}\\Specs.html ${test_assembly}")

target default, (test):
	pass