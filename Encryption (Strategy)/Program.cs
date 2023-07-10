using Encryption__Strategy_.IEncryption;

IEncryption<string> a = new MatrixCipher();
string input = "Generate Random Number in Min to Max Range!";
string encryption = a.encrypt(input);
Console.WriteLine(encryption);
Console.WriteLine(a.decrypt(encryption));



