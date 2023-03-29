#ifndef CHECKER_HPP
#define CHECKER_HPP
	#include <string>
	#include <stdio.h>
	#include "hashpp.h"
	//#define MAX_SIZE_FOR_STACK 16382
	//#include <iostream>
	//#include <iomanip>
	//#include <fstream>
	//#include <string>
	//#include <openssl/md5.h>

	//#include <sstream>
	//#include <iomanip>

	extern "C" {

		__declspec(dllexport) bool CheckPatch(const char* path, const char* patchName, const char* md5);

		__declspec(dllexport) bool DownloadPatch(const char* path, const char* name);

		__declspec(dllexport) const char* GetMD5(const char* path, const char* patchName);
	}

#endif // !CHECKER_HPP





