#ifndef CHECKER_HPP
#define CHECKER_HPP
	#include <string>
	#include <stdio.h>



	extern "C" {
		//return params
		// true no need update
		// false need update
		__declspec(dllexport) bool CheckPatch(const char* path, const char* patchName);
		//{
			//return 1;
		//};
		//return params
		// true updated
		// false cant update
		__declspec(dllexport) bool DownloadPatch(const char* path, const char* name);
		//{
		//	return 1;
		//};
	}

#endif // !CHECKER_HPP





