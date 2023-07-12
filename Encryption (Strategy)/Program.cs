using Encryption__Strategy_.IEncryption;
using Encryption__Strategy_.Encryptor;
using Encryption__Strategy_.Encryptor.TextEncryptor;
using Encryption__Strategy_.Encryptor.FileEncryptor;

// -------- 1 -----------
FileEncryptor a = new("KEYAKA", "./Output.TXT"); 
a.Decrypt(new FileInfo("./Input.TXT"));


// -------- 2 -----------
//IEncryption<string> fourSquares = new FourSquaresCipher();

//TextEncryptor a = new(fourSquares);

//string cipher = a.Encrypt("The Dictionary can be accessed using indexer.");
//Console.WriteLine("Encrypted by Four Squares: " + cipher);

//a.SetStrategy(new PairCipher());
//cipher = a.Encrypt(cipher);
//Console.WriteLine("Encrypted by Pairs Cipher: " + cipher);

//cipher = a.Decrypt(cipher);
//Console.WriteLine("Decrypted by Pairs Cipher: " + cipher);

//a.SetStrategy(fourSquares);
//Console.WriteLine("Decrypted by Four Squares: " + a.Decrypt(cipher));