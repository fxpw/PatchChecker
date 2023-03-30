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
	typedef void(_stdcall* LPEXTFUNCRESPOND) (LPCSTR s);


	extern "C" {

		__declspec(dllexport) bool CheckPatch(const char* pathToFile, const char* md5);


		__declspec(dllexport) void __stdcall GetMD5(const char* path, const char* patchName, LPEXTFUNCRESPOND respond);

	}

#endif // !CHECKER_HPP





