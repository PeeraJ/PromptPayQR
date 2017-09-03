# PromptPayQR

With PromptPayQR you can generate QR Code that accepted by following Bank

- Bank of Ayudhya
-  Bangkok Bank
-  Kasikorn Bank
-  Krung Thai Bank
- Siam Commercial Bank
- Thanachart Bank

### Example Code

to get PromptPay QRCode payload string use following code ...

```Csharp
var qrCode = PromptPayQR.QRCodePayload(promptpayID, amount);
```

to get bitmap use for rendering on your existing application use following code...
```Csharp
var qrCode = PromptPayQR.QRCodePayload(promptpayID, amount);
var qrCodeBitmap = PromptPayQR.QRCodeImage(qrCode);
```
or just 
```Csharp
var qrCodeBitmap = PromptPayQR.QRCodeImage(promptpayID, amount);
```
##### bring to you by..

>Peera Jeawkok
>Peera@live.it
Software Engineer
>AppMan Co.,Ltd

##### License
GNU General Public License V3
http://www.gnu.org/licenses/gpl-3.0.html
