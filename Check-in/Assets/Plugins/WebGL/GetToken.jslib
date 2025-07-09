mergeInto(LibraryManager.library, {
    GetTokenFromHeader: function () {
        var token = "namdeptrai";
        if (typeof window !== 'undefined' && window.token) {
            token = window.token;
        }
        
        var lengthBytes = lengthBytesUTF8(token) + 1;
        var stringOnWasmHeap = _malloc(lengthBytes);
        stringToUTF8(token, stringOnWasmHeap, lengthBytes);
        return stringOnWasmHeap;
    }
});