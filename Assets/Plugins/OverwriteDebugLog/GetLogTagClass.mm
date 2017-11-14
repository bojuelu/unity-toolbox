// Refrence from: https://stackoverflow.com/questions/37047781/how-to-return-string-from-native-ios-plugin-to-unity
// Author: BoJue
// Orig-Author: Cabrra https://stackoverflow.com/users/6162065/cabrra
// Orig-Author say, He found code from: http://blog.mediarain.com/2013/03/creating-ios-plugins-for-unity/

@implementation GetLogTagClass : UIViewController

@end

char* cStringCopy(const char* string)
{
    if (string == NULL)
        return NULL;
    
    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    
    return res;
}

// This takes a char* you get from Unity and converts it to an NSString* to use in your objective c code. You can mix c++ and objective c all in the same file.
static NSString* CreateNSString(const char* string)
{
    if (string != NULL)
        return [NSString stringWithUTF8String:string];
    else
        return [NSString stringWithUTF8String:""];
}



extern "C"
{
    char* GetLogTag()
    {
        NSString *logTag = @"LOG_NONE";  // { LOG_VERBOSE || LOG_DEBUG || LOG_WARNING || LOG_ERROR || LOG_EXCEPTION }
        
        return cStringCopy([logTag UTF8String]);
    }
}

