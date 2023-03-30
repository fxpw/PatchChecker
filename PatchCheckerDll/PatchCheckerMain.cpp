#include "pch.h"
#include "PatchCheckerMain.hpp"

//using namespace hashpp;

__declspec(dllexport) bool CheckPatch(const char* pathToFile, const char* md5) {

    std::string nmd5 = std::string(md5);

    auto hash = hashpp::get::getFileHash(hashpp::ALGORITHMS::MD5, pathToFile);
    std::string hstr = hash.getString();
    std::transform(hstr.begin(), hstr.end(), hstr.begin(), toupper);
    //std::cout << pathToFile << " ********CheckPatch********** " << std::endl;
    //std::cout << hstr << " md5 from file " << std::endl;
    //std::cout << nmd5 << " md5 from json " << std::endl;
    //std::cout << " Check patch returned" << ((hstr == nmd5) ? " true" : " false ") << std::endl;
    //std::cout << pathToFile << " **********end******** " << std::endl;

    return hstr == nmd5;
}


__declspec(dllexport) void __stdcall GetMD5(const char* path, const char* patchName, LPEXTFUNCRESPOND respond) {

    auto hash = hashpp::get::getFileHash(hashpp::ALGORITHMS::MD5, patchName);

    std::string forReturn = hash.getString();
    //std::cout << forReturn << " returned md5 from GetMD5()" << std::endl;
    //const char* fr = forReturn.c_str();
    respond(forReturn.c_str());

}

