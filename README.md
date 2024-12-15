# Crazy Musicians API

Crazy Musicians API, ürünlerin (müzisyenler) bilgilerini yöneten bir basit RESTful API'dir. Bu API, ürünleri listelemek, detaylarını görmek ve yeni ürünler eklemek için kullanılabilir.

## Özellikler

- **GET /api/products**: Tüm ürünleri (müzisyenleri) listeler.
- **GET /api/products/by-id/{id}**: Belirli bir ürünün detaylarını getirir.
- **POST /api/products**: Yeni bir ürün (müzisyen) ekler.

## Gereksinimler

- .NET 6 SDK veya daha yeni bir sürüm
- Visual Studio veya herhangi bir IDE/Text Editor
- Bir REST API istemcisi (Postman, cURL, vs.) veya tarayıcı

## Kurulum

1. **Projeyi Klonlayın**:
   ```bash
   git clone https://github.com/username/crazy-musicians-api.git
   cd crazy-musicians-api
   ```

2. **Bağımlılıkları Yükleyin**:
   Projede kullanılan NuGet paketlerini yüklemek için aşağıdaki komutu çalıştırın:
   ```bash
   dotnet restore
   ```

3. **Uygulamayı Çalıştırın**:
   ```bash
   dotnet run
   ```

4. **Swagger Arayüzüne Erişin**:
   Tarayıcınızda şu URL'yi açarak API endpoint'lerini test edebilirsiniz:
   ```
   http://localhost:5295/swagger/index.html
   ```

## Kullanım

### **GET /api/products**
Tüm ürünleri listeler.
#### Örnek Yanıt:
```json
[
  {
    "id": 1,
    "name": "Product 1",
    "age": 30,
    "numberOfAlbums": 5
  },
  {
    "id": 2,
    "name": "Product 2",
    "age": 25,
    "numberOfAlbums": 3
  }
]
```

### **GET /api/products/by-id/{id}**
Belirli bir ürünün detaylarını getirir.
#### Örnek İstek:
```bash
GET /api/products/by-id/1
```
#### Örnek Yanıt:
```json
{
  "id": 1,
  "name": "Product 1",
  "age": 30,
  "numberOfAlbums": 5
}
```

### **POST /api/products**
Yeni bir ürün ekler.
#### Örnek İstek:
```bash
POST /api/products
Content-Type: application/json

{
  "name": "New Product",
  "age": 22,
  "numberOfAlbums": 4
}
```
#### Örnek Yanıt:
```json
{
  "id": 11,
  "name": "New Product",
  "age": 22,
  "numberOfAlbums": 4
}
```

## Proje Yapısı

- **Controllers**: API için controller'lar burada tanımlıdır.
- **Models**: API'de kullanılan veri modelleri burada bulunur.
- **Program.cs**: Uygulama başlangıç ayarlarını içerir.

## Hata Ayıklama

- Eğer uygulama çalışmıyorsa, port çakışmalarını kontrol edin. `launchSettings.json` dosyasından portu değiştirebilirsiniz.
- API'ye erişim sağlayamıyorsanız Swagger arayüzünü kontrol edin.

## Lisans

Bu proje MIT Lisansı ile lisanslanmıştır. Daha fazla bilgi için LICENSE dosyasına bakınız.
