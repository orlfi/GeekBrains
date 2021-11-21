using Task_1.Codecs;

var sourceText = "abcdefABCDEFабвгдеАБВГДЕ0123";
Console.WriteLine("Source string: " + sourceText + "\r\n");
var aCoder = new ACoder();
var aEncodedText = aCoder.Code(sourceText);
Console.WriteLine("AEncoded: " + aEncodedText);
var aDecodedText = aCoder.Decode(aEncodedText);
Console.WriteLine("ADecoded: " + aDecodedText + "\r\n");

var bCoder = new BCoder();
var bEncodedText = bCoder.Code(sourceText);
Console.WriteLine("BEncoded: " + bEncodedText);
var bDecodedText = bCoder.Decode(bEncodedText);
Console.WriteLine("BDecoded: " + bDecodedText);