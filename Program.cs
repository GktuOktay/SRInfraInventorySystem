Console.WriteLine("SOHBET İLE TEST BAŞLIYOR");
Console.WriteLine("Akıllı");
Console.WriteLine("Ar-Ge");  
Console.WriteLine("Elektronik");
Console.WriteLine("Yazılım");

// Alfabetik sıralama testi
var testNames = new List<string>
{
    "Yazılım Geliştirme Şefliği",
    "Akıllı Şehircilik ve Coğrafi Bilgi Sistemleri Şefliği", 
    "Elektronik Sistemler Tasarım Şefliği",
    "Ar-Ge Projeleri Yönetim Şefliği"
};

Console.WriteLine("\nSıralama öncesi:");
foreach (var name in testNames)
{
    Console.WriteLine($"- {name}");
}

var sorted = testNames.OrderBy(x => x, StringComparer.CurrentCultureIgnoreCase).ToList();

Console.WriteLine("\nSıralama sonrası:");
foreach (var name in sorted)
{
    Console.WriteLine($"- {name}");
} 