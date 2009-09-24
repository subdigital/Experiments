import System.IO

build_dir = "build"

target init:
	rmdir(build_dir)
	mkdir(build_dir)

target default, (init):
	pass