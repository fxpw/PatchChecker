#include "pch.h"
#include "PatchCheckerMain.hpp"

using namespace hashpp;

__declspec(dllexport) bool CheckPatch(const char* path, const char* patchName, const char* md5) {

    std::string nmd5 = std::string(md5);

    auto hash = get::getFileHash(ALGORITHMS::MD5, patchName);
    std::string hstr = hash.getString();
    std::transform(hstr.begin(), hstr.end(), hstr.begin(), toupper);

    return hstr == nmd5;

}

__declspec(dllexport) bool DownloadPatch(const char* path, const char* patchName) {
    return false;
}

__declspec(dllexport) const char* GetMD5(const char* path, const char* patchName) {


    auto hash = get::getFileHash(ALGORITHMS::MD5, patchName);

    std::string forReturn = hash.getString();
    return forReturn.c_str();

}
