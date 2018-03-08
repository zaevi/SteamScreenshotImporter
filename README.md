# Steam Screenshot Importer

导入非Steam截图至Steam中

![预览](https://user-images.githubusercontent.com/12966814/37142156-383e9aea-22f2-11e8-9785-58b8b7d355e3.png)

## 特性

- 自动检索Steam目录并获取用户和游戏信息
- 支持导入至非本地游戏
- 支持批量图像导入, 支持拖拽

## 使用

- 下载[SteamScreenshotImporter](https://github.com/Zaeworks/SteamScreenshotImporter/releases)
- 使用之前先确保Steam在此机器上至少截了一张图, 随便开个游戏按下F12就好
- 打开程序, 自动扫描Steam信息, 如果注册表中没有Steam注册信息请手动选择Steam根目录
- 选择要导入至的用户和游戏
- 将要导入的图像拖拽至程序中
- 导入! 然后重启Steam

## 依赖

- 用于解析Valve Data Format文件`.vdf`的[Gameloop.Vdf](https://github.com/shravan2x/Gameloop.Vdf)

## 更新

### v1.2

- **支持导入截图至非本地游戏中**

  `第一次扫描时需通过Steam Web API拉取全部游戏列表, 会有一些卡顿 `

- 优化性能

### v1.1

- 修复截图无法上传的问题
`Steam在screenshots.vdf中自动添入的截图信息导致非Steam无法上传, 故改由本程序在导入截图时添入截图记录`

- 修改缩略图尺寸为固定宽200
