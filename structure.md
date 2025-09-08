临时markdown文件，记录代码结构。

---

### Lunar.Core.Base
Core.Base 是 Lunar 对游戏引擎类的抽象——实现跨引擎的基础。

**不依赖于任何外部库。**

### Lunar.Core.ECS
Core.ECS 是 Lunar 基于 ArchECS 提供的一套ECS实现的基类。

**仅依赖于 ArchECS。**

- Components: 定义基础组件类型。
- Interfaces: 行为服务接口。
- Systems: 定义 ECS 中的 System 基类。

### Lunar.Modules
模块应只做一件事情，且模块非单例。

跨模块访问时，只能访问 API 层； 跨模块 调用 / 依赖 尽量都设为“可选的”， 组合逻辑放在更高层的 Package 中。

**仅依赖于 Core。**

- TypeWriter
- Item
- ...

#### 通用结构
  - API: 对外公开的接口、数据结构、入口类
  - Core: 全部可复用的纯业务逻辑
  - Internal: 私有实现细节（可能经常变动、不适合暴露）

### Lunar.Adapters.Unity
Unity Engine 的桥接库。

**仅依赖于 Core 与 Unity API。**

- Adapters: 对 Core.ECS.Interfaces 的具体实现
- Systems: 对 Core.ECS.System 的具体实现
- Utils: 静态实用方法
- (opt)Editors