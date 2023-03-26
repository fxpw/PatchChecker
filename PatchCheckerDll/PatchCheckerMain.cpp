#include "pch.h"
#include "PatchCheckerMain.hpp"

//return params
// true no need update
// false need update
__declspec(dllexport) bool CheckPatch(const char* path, const char* patchName){
	return false;
}

//return params
// true updated
// false cant update
__declspec(dllexport) bool DownloadPatch(const char* path, const char* patchName){
	return false;
}
