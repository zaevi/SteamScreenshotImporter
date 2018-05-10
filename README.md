# Steam Screenshot Importer

Import images to Steam screenshot library.

![Preview](https://user-images.githubusercontent.com/12966814/39874764-5343c226-54a1-11e8-9560-8ae430c390d4.png)

### Features

- Scan Steam data automatically
- Support importing images to all your apps (including non-local apps)
- Multi-importing support, dragging support

### Usage

- Download [SteamScreenshotImporter](https://github.com/Zaeworks/SteamScreenshotImporter/releases)
- Make sure your Steam has captured any screenshot once before (to generate `Steam/userdata/{userid}/760/screenshots.vdf`)
- Open this application, it will scan Steam user data, and fetch app name list (may take times)
- Select user and app, and drag your images into
- Import! If Steam is running now, just restart it after importing

### Required Package

- [Gameloop.Vdf](https://github.com/shravan2x/Gameloop.Vdf) to parse Value Data Format file `.vdf` 

### Log

####v1.3

- Multi-language support

#### v1.2

- Non-local app importing support


#### v1.1

- Fixed - can't upload imported images
  `screenshots.vdf has to be updated after import`

- Change thumbnail's width to 200px
