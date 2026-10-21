namespace Extensions {
    static class JSONFormatExtension {
        public static bool IsJSON(this string fileText) {
            return fileText.Trim().StartsWith('{') && fileText.Trim().EndsWith('}') || (fileText.Trim().StartsWith('[') && fileText.Trim().EndsWith(']'));
        }
    }
}