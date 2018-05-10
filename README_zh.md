# Steam Screenshot Importer

导入非Steam截图至Steam中

![预览](https://user-images.githubusercontent.com/12966814/39874764-5343c226-54a1-11e8-9560-8ae430c390d4.png)

### 特性

- 自动检索Steam目录并获取用户和游戏信息
- 支持导入至所有用户游戏(包括非本地游戏)
- 支持批量图像导入, 支持拖拽

### 使用

- 下载[SteamScreenshotImporter](https://github.com/Zaeworks/SteamScreenshotImporter/releases)
- 使用之前先确保Steam在此机器上至少截了一张图(生成`Steam/userdata/{userid}/760/screenshots.vdf`), 随便开个游戏按下F12就好
- 打开程序, 自动扫描Steam信息, 程序会拉取游戏名称列表(第一次会有些卡顿)
- 选择要导入至的用户和游戏, 然后把要导入的图片拖进来
- 导入! 如果Steam正在运行, 重启之后生效

### 依赖

- 用于解析Valve Data Format文件`.vdf`的[Gameloop.Vdf](https://github.com/shravan2x/Gameloop.Vdf)

### 更新

#### v1.3

- 多语言支持, 目前非中文地区显示英文

#### v1.2

- 支持导入截图至非本地游戏中


#### v1.1

- 修复截图无法上传的问题
`Steam在screenshots.vdf中自动添入的截图信息导致非Steam无法上传, 故改由本程序在导入截图时添入截图记录`

- 修改缩略图尺寸为固定宽200

