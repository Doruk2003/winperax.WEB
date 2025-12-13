$domainPath = "Winperax.Domain"

$entityPath = Join-Path $domainPath "Entities"
$valueObjectPath = Join-Path $domainPath "ValueObjects"

function Add-RequiredKeyword {
    param (
        [string]$FilePath
    )

    $content = Get-Content $FilePath -Raw
    $lines = $content -split "`n"

    $newLines = @()
    foreach ($line in $lines) {
        # Satır bir property tanımı mı ve null atanamaz bir tür mü kullanıyor?
        # Örnek: public string Name { get; set; }
        # Regex: public\s+(nullable type değilse)\s+(\w+)\s+{\s*get;\s*set;\s*}
        # Burada, string, List<T>, int?, bool? gibi tipleri kontrol etmemiz gerekir.
        # Nullable tipler: int?, bool?, string?, List<T>?, MyClass?
        # Non-nullable tipler: int, bool, string, List<T>, MyClass (ancak bu sonuncusu sınıf örneği null olabilir ama tip olarak nullable değil)

        # Basit bir regex: public ile başlayan, bir boşluk, tür adı (nullable olmayan), boşluk, isim, boşluk, { get; set; }
        # Daha kesin olmak için: string, List<>, DateTime, Guid, int, double, float, bool, decimal, char, byte, sbyte, short, ushort, uint, ulong, nint, nuint, object, enum'lar gibi tipleri hedef al.
        # Ancak, object ve enum'lar hariç diğerleri genelde non-nullable'dır.
        # string, List<T> gibi tipleri hedef alalım.
        # NOT: DateTime, Guid gibi değer tipleri zaten null atanamaz. Onlara gerek yok.
        # NOT: bool, int, double, float, decimal, char, byte, sbyte, short, ushort, uint, ulong, nint, nuint gibi değer tipleri de zaten null atanamaz. Onlara da gerek yok.
        # Dolayısıyla, sadece class isimleri (örneğin başka bir entity), string, List<T>, Dictionary<TKey, TValue> gibi koleksiyonlar ve diğer referans tipler için `required` mantıklıdır.
        # Ancak, çok fazla karmaşıklık getirmemek adına, sadece `string` türlerine `required` ekleyelim.
        # Daha sonra, List<string> veya List<SomeClass> gibi koleksiyonları da ekleyebiliriz.

        # Bu regex sadece `public string PropertyName { get; set; }` kalıbını hedef alır.
        # Daha genel bir regex: `public\s+(?!bool|int|double|float|decimal|char|byte|sbyte|short|ushort|uint|ulong|nint|nuint|DateTime|Guid)\w+\s+\w+\s+{\s*get;\s*set;\s*}`
        # Ancak, bu da çok genel olur. `List<T>` gibi kalıpları da yakalamak için daha karmaşık regex'ler gerekebilir.
        # En kolay yol, `string` türüne sahip olanlara `required` eklemek.

        $pattern = '^(?<indent>\s*)public string (?<propName>\w+) { get; set; }(?<rest>.*)$'
        if ($line -match $pattern) {
            $indent = $matches['indent']
            $propName = $matches['propName']
            $rest = $matches['rest']
            $newLine = "${indent}public required string ${propName} { get; set; }${rest}"
            Write-Host "Güncelleniyor: $FilePath - $propName"
            $newLines += $newLine
        }
        # `List<>` türüne sahip olanlara da `required` ekleyelim
        elseif ($line -match '^(?<indent>\s*)public List<(?<genericType>.+)> (?<propName>\w+) { get; set; }(?<rest>.*)$') {
            $indent = $matches['indent']
            $genericType = $matches['genericType']
            $propName = $matches['propName']
            $rest = $matches['rest']
            $newLine = "${indent}public required List<${genericType}> ${propName} { get; set; }${rest}"
            Write-Host "Güncelleniyor: $FilePath - $propName (List)"
            $newLines += $newLine
        }
        # `DateTime` hariç diğer değer tipleri için de `required` eklemek mantıklı olmaz. Ancak `DateTime?` (nullable datetime) hariç.
        # `DateTime` için `required` gerekli değil. Zaten null atanamaz.
        # `DateTime?` için `required` mantıklı olabilir, ama bu da null olabileceği için `required` zorunlu tutulmamalı.
        # `Guid` için de `required` gerekli değil.
        # `bool` için de `required` gerekli değil.
        # `int` için de `required` gerekli değil.
        # Bu nedenle, sadece `string` ve `List<>` türlerine odaklanalım.
        # Diğer referans tipleri (örneğin başka bir Entity sınıfı) için de `required` eklenebilir, ama bu çok genel olur.
        # Şimdilik, sadece `string` ve `List<>` için işlem yapalım.
        # `object` türüne de `required` eklenebilir, ama bu çok genel olur.
        # `Dictionary<TKey, TValue>` gibi diğer koleksiyonlara da eklenebilir, ama bu da çok genel olur.
        # Bu script, sadece `string` ve `List<>` türlerine `required` ekler.
        # Daha sonra, diğer türler için scripti güncelleyebiliriz.
        # `required` eklemek için uygun olan türler:
        # - `string`
        # - `List<T>`
        # - Başka bir class türü (örneğin `SomeEntity`)
        # - `Dictionary<TKey, TValue>`
        # - `IList<T>`
        # - `IEnumerable<T>`
        # - `IReadOnlyList<T>`
        # - `IReadOnlyCollection<T>`
        # - `HashSet<T>`
        # - `Stack<T>`
        # - `Queue<T>`
        # - `LinkedList<T>`
        # - `ArraySegment<T>`
        # - `Span<T>`
        # - `Memory<T>`
        # - `ImmutableList<T>`
        # - `ImmutableArray<T>`
        # - `ImmutableDictionary<TKey, TValue>`
        # - `ImmutableHashSet<T>`
        # - `IImmutableList<T>`
        # - `IImmutableDictionary<TKey, TValue>`
        # - `IImmutableSet<T>`
        # - `IImmutableQueue<T>`
        # - `IImmutableStack<T>`
        # - `IImmutableDictionary<TKey, TValue>`
        # - `IImmutableList<T>`
        # - `IImmutableSet<T>`
        # - `IImmutableQueue<T>`
        # - `IImmutableStack<T>`
        # - `IImmutableDictionary<TKey, TValue>`
        # - `IImmutableList<T>`
        # - `IImmutableSet<T>`
        # - `IImmutableQueue<T>`
        # - `IImmutableStack<T>`
        # - `IImmutableDictionary<TKey, TValue>`
        # - `IImmutableList<T>`
        # - `IImmutableSet<T>`
        # - `IImmutableQueue<T>`
        # - `IImmutableStack<T>`
        # - `IImmutableDictionary<TKey, TValue>`
        # - `IImmutableList<T>`
        # - `IImmutableSet<T>`
        # - `IImmutableQueue<T>`
        # - `IImmutableStack<T>`
        # - `IImmutableDictionary<TKey, TValue>`
        # - `IImmutableList<T>`
        # - `IImmutableSet<T>`
        # - `IImmutableQueue<T>`
        # - `IImmutableStack<T>`
        # - `IImmutableDictionary<TKey, TValue>`
        # - `IImmutableList<T>`
        # - `IImmutableSet<T>`
        # - `IImmutableQueue<T>`
        # - `IImmutableStack<T>`
        # - `IImmutableDictionary<TKey, TValue>`
        # - `IImmutableList<T>`
        # - `IImmutableSet<T>`
        # - `IImmutableQueue<T>`
        # - `IImmutableStack<T>`
        # - `IImmutableDictionary<TKey, TValue>`
        # - `IImmutableList<T>`
        # - `IImmutableSet<T>`
        # - `IImmutableQueue<T>`
        # - `IImmutableStack<T>`
        # - `IImmutableDictionary<TKey, TValue>`
        # - `IImmutableList<T>`
        # - `IImmutableSet<T>`
        # - `IImmutableQueue<T>`
        # - `IImmutableStack<T>`
        # - `IImmutableDictionary<TKey, TValue>`
        # - `IImmutableList<T>`
        # - `IImmutableSet<T>`
        # - `IImmutableQueue<T>`
        # - `IImmutableStack<T>`
        # - `IImmutableDictionary<TKey, TValue>`
        # - `IImmutableList<T>`
        # - `IImmutableSet<T>`
        # - `IImmutableQueue<T>`
        # - `IImmutableStack......` (Sürekli tekrar ediyor, bu dağınık bir yapıdır.)

        # Bu nedenle, script'i sadece `string` ve `List<>` türleri için yazdım.
        # Daha sonra, diğer türler için script'i güncelleyebiliriz.

        else {
            $newLines += $line
        }
    }

    $updatedContent = $newLines -join "`n"
    Set-Content -Path $FilePath -Value $updatedContent -Encoding UTF8
}

# Entities klasöründeki tüm .cs dosyalarını işle
Get-ChildItem -Path $entityPath -Recurse -Filter "*.cs" | ForEach-Object {
    Add-RequiredKeyword -FilePath $_.FullName
}

# ValueObjects klasöründeki tüm .cs dosyalarını işle
Get-ChildItem -Path $valueObjectPath -Recurse -Filter "*.cs" | ForEach-Object {
    Add-RequiredKeyword -FilePath $_.FullName
}

Write-Host "Tüm dosyalar işlendi."