# CLAUDE.md — Project Intelligence File
> Đọc file này **trước tiên** mỗi khi bắt đầu làm việc với project.  
> Dành cho: Claude Code · Claude Cowork · Cursor · Copilot  
> Cập nhật lần cuối: xem `## CHANGELOG`

---

## 📌 MỤC LỤC

1. [Project Overview](#1-project-overview)
2. [Architecture Manual](#2-architecture-manual)
3. [Coding Standards & Comment Rules](#3-coding-standards--comment-rules)
4. [Session Memory & Context Linking](#4-session-memory--context-linking)
5. [Changelog — Edit Log](#5-changelog--edit-log)
6. [Unit Test Guidelines](#6-unit-test-guidelines)
7. [Performance Optimization Rules](#7-performance-optimization-rules)
8. [How to Use This File](#8-how-to-use-this-file)

---

## 1. PROJECT OVERVIEW

```yaml
project_name:     "<Cut_Sheet>"
version:          "0.1.0"
language:         "C#"
framework:        "Winform"
package_manager:  "raspberry|Linux|Mono|Modbus TCP/IP"
primary_author:   ""
repo:             ""
env:              "development"   # development | staging | production
```

### Mục tiêu
> Mô tả ngắn gọn: project này giải quyết vấn đề gì, cho ai, theo cách nào.

### Ràng buộc quan trọng
- [ ] Không dùng thư viện X vì lý do Y
- [ ] Mọi API call phải qua `src/lib/api.ts`
- [ ] Không commit secret / key trực tiếp vào code

---

## 2. ARCHITECTURE MANUAL

### 2.1 Sơ đồ thư mục

```
project-root/
├── src/
│   ├── components/       # UI components (dumb, presentational)
│   ├── containers/       # Smart components / page-level logic
│   ├── hooks/            # Custom React hooks
│   ├── lib/              # Shared utilities, API clients
│   ├── services/         # Business logic, external integrations
│   ├── store/            # State management (Redux / Zustand / Jotai)
│   ├── types/            # Global TypeScript types / interfaces
│   └── utils/            # Pure helper functions
├── tests/
│   ├── unit/             # Unit tests (*.test.ts)
│   ├── integration/      # Integration tests
│   └── e2e/              # End-to-end tests (Playwright / Cypress)
├── docs/                 # Architecture diagrams, ADRs
├── scripts/              # Dev/build/deploy scripts
├── CLAUDE.md             # ← File này
└── ...
```

### 2.2 Luồng dữ liệu (Data Flow)

```
[UI Component]
     │ dispatch / call hook
     ▼
[Store / Hook]
     │ calls
     ▼
[Service Layer]   ←── chứa toàn bộ business logic
     │ calls
     ▼
[API Client / lib/api.ts]
     │ HTTP / SDK
     ▼
[External API / DB]
```

### 2.3 Quy tắc phân tầng (Layer Rules)

| Layer       | Được phép import              | Không được import   |
|-------------|-------------------------------|----------------------|
| components  | hooks, types, utils           | services, store trực tiếp |
| hooks       | store, services, utils        | components           |
| services    | lib, utils, types             | components, hooks    |
| lib/api     | types                         | mọi layer khác       |

### 2.4 Quyết định kiến trúc (ADR — Architecture Decision Records)

| ID    | Ngày       | Quyết định                        | Lý do                        | Trạng thái  |
|-------|------------|-----------------------------------|------------------------------|-------------|
| ADR-1 | YYYY-MM-DD | Dùng Zustand thay Redux           | Bundle nhỏ hơn, API đơn giản | Accepted    |
| ADR-2 | YYYY-MM-DD | Tất cả fetch qua `lib/api.ts`     | Centralize error handling    | Accepted    |
<!-- Thêm ADR mới vào đây -->

---

## 3. CODING STANDARDS & COMMENT RULES

### 3.1 Cấu trúc comment bắt buộc

#### File header (mọi file source)
```typescript
/**
 * @file        src/services/userService.ts
 * @description Xử lý nghiệp vụ liên quan đến User: CRUD, auth, profile.
 * @author      <Tên> <email>
 * @created     YYYY-MM-DD
 * @modified    YYYY-MM-DD — <mô tả thay đổi ngắn>
 * @see         docs/user-flow.md
 */
```

#### Function / Method
```typescript
/**
 * Lấy thông tin user theo ID từ database.
 *
 * @param   {string}          userId  - UUID của user cần tìm
 * @param   {FetchOptions}    opts    - Tuỳ chọn cache / force-refresh
 * @returns {Promise<User>}           - Object User hoặc throw nếu không tìm thấy
 * @throws  {NotFoundError}           - Khi userId không tồn tại
 *
 * @example
 *   const user = await getUser("abc-123");
 *   console.log(user.name);
 */
async function getUser(userId: string, opts?: FetchOptions): Promise<User> { ... }
```

#### Inline comment — chỉ dùng khi logic KHÔNG tự giải thích được
```typescript
// ✅ Đúng: giải thích tại sao, không phải cái gì
const delay = 350; // Debounce 350ms — dưới ngưỡng này user chưa dừng gõ

// ❌ Sai: lặp lại code
const delay = 350; // gán delay bằng 350
```

#### TODO / FIXME / HACK
```typescript
// TODO(username, YYYY-MM-DD): Migrate sang API v2 sau khi backend deploy
// FIXME(username, YYYY-MM-DD): Race condition khi 2 tab cùng gọi refresh
// HACK(username, YYYY-MM-DD): Workaround bug iOS Safari — xoá khi upgrade lib
// PERF(username, YYYY-MM-DD): Bottleneck ở đây — xem CHANGELOG#PERF-001
```

### 3.2 Naming conventions

| Loại            | Convention         | Ví dụ                        |
|-----------------|--------------------|------------------------------|
| Variable/Param  | camelCase          | `userId`, `fetchOptions`     |
| Function        | camelCase verb     | `getUser`, `handleSubmit`    |
| Class/Interface | PascalCase         | `UserService`, `FetchOptions`|
| Constant        | UPPER_SNAKE_CASE   | `MAX_RETRY`, `API_BASE_URL`  |
| File (TS/JS)    | kebab-case         | `user-service.ts`            |
| CSS class       | kebab-case / BEM   | `btn--primary`               |
| Test file       | `*.test.ts`        | `user-service.test.ts`       |

### 3.3 Code style tóm tắt

```typescript
// Max line length: 100 chars
// Indent: 2 spaces (không dùng tab)
// Semicolons: bắt buộc (TypeScript)
// Single quote cho strings
// Trailing comma trong multi-line objects/arrays
// Arrow function cho callbacks
// async/await — không dùng .then().catch() thuần (trừ khi chain phức tạp)
```

---

## 4. SESSION MEMORY & CONTEXT LINKING

> **Mục đích:** Giúp Claude duy trì ngữ cảnh giữa các phiên làm việc mà không cần đọc lại toàn bộ code.

### 4.1 Active Context — Việc đang làm

```yaml
# Cập nhật phần này MỖI KHI kết thúc session làm việc
active_context:
  current_task: "Fix FormConfig: _btnSave_Click không lưu tài khoản + thiếu try-catch password"

  related_files:
    - "sourcecode/Cut_Sheet/Cut_Sheet/FormConfig.cs"

  blocked_by: ""

  next_step: >
    Bug đã fix. Test checklist:
    - Đổi password → click Lưu (KHÔNG click "Cập nhật tài khoản") → đóng → mở lại → đăng nhập bằng password mới → phải thành công
    - Đổi password mismatch → click Lưu → phải thấy lỗi "không khớp", không đóng form
    - LƯU Ý debug từ VS: mỗi lần Build, VS copy App.config gốc đè bin/Debug/Cut_Sheet.exe.config → mất config đã lưu. KHÔNG phải bug code.
    - Xác nhận với backend mapping: QR2→prepregItem, QR1→prepregOrderItem
    - Kiểm tra API có cần header xác thực không

  last_session: "2026-05-27"

  open_questions:
    - "API 192.168.96.10 có yêu cầu header xác thực không?"
    - "Xác nhận mapping: QR2→prepregItem, QR1→prepregOrderItem — đúng không?"
```

### 4.2 Quyết định đã chốt (Decision Log)

| ID     | Ngày       | Quyết định                              | Ai quyết | File liên quan              |
|--------|------------|-----------------------------------------|----------|-----------------------------|
| DEC-001 | YYYY-MM-DD | Dùng JWT lưu trong httpOnly cookie      | Team     | `lib/auth.ts`               |
| DEC-002 | YYYY-MM-DD | Không dùng ORM, viết raw SQL qua pg     | Dev      | `lib/db.ts`                 |
<!-- Thêm quyết định mới vào đây -->

### 4.3 Hướng dẫn Claude đọc context

Khi bắt đầu session mới, Claude PHẢI:
1. Đọc `active_context` → biết đang làm gì
2. Đọc `CHANGELOG` gần nhất → biết đã thay đổi gì
3. Đọc `Decision Log` → tránh đề xuất lại phương án đã bác bỏ
4. **Không** hỏi lại những gì đã ghi trong file này

**Prompt mẫu để bắt đầu session:**
```
Đọc CLAUDE.md và tiếp tục từ active_context. 
Task hiện tại: [mô tả]. File cần làm việc: [list file].
```

---

## 5. CHANGELOG — EDIT LOG

> Ghi lại **mọi thay đổi đáng kể** theo thứ tự ngược (mới nhất lên đầu).  
> Format: `[YYYY-MM-DD] [TYPE] [File/Module] — Mô tả`  
> Types: `FEAT` · `FIX` · `REFACTOR` · `PERF` · `TEST` · `DOCS` · `CHORE` · `BREAK`

---

### [YYYY-MM-DD] — Session N

```
[FEAT]     src/services/userService.ts     — Thêm hàm getUser() với cache
[FIX]      src/hooks/useAuth.ts            — Sửa race condition khi logout
[TEST]     tests/unit/userService.test.ts  — Thêm 8 test case cho getUser()
[DOCS]     CLAUDE.md                       — Cập nhật active_context
```

**Chi tiết nếu cần:**
- `getUser()`: Thêm `staleTime: 5 phút`, fallback sang localStorage khi offline
- `useAuth`: Lock bằng `ref` flag, tránh double-call `/refresh`

---

### [YYYY-MM-DD] — Session N-1

```
[CHORE]    package.json    — Upgrade Zod từ 3.21 → 3.23
[REFACTOR] lib/api.ts      — Tách error handler thành hàm riêng handleApiError()
```

### [2026-05-27] — Session: Fix FormConfig Save + Password

```
[FIX]   FormConfig.cs   — _btnSave_Click: thêm lưu tài khoản (username + password nếu được nhập)
[FIX]   FormConfig.cs   — _btnSave_Click: validate password match TRƯỚC khi lưu bất cứ thứ gì
[FIX]   FormConfig.cs   — _btnChangePassword_Click: thêm try-catch, báo lỗi nếu ghi file thất bại
```

**Root cause:**
- `_btnSave_Click` chỉ gọi `SaveQrPatterns()` + `SaveErpSettings()` — không lưu tab Tài khoản
- User nhập password mới → click "Lưu" → báo thành công nhưng password KHÔNG thay đổi
- `_btnChangePassword_Click` thiếu try-catch: nếu ghi file lỗi (quyền, lock...), WinForms nuốt exception, user không biết

**Behavior sau fix:**
- "Lưu" lưu TẤT CẢ: QR patterns + ERP + username + password (nếu được nhập)
- "Cập nhật tài khoản" vẫn hoạt động như cũ (lưu tức thì, không cần nhấn Lưu thêm)
- Nếu password mới không khớp xác nhận → báo lỗi, không đóng form, không lưu gì cả

---

### [2026-05-27] — Session: API Badge UI + Retry Queue

```
[FEAT]   Form1.cs   — Thêm badge Label (_labApiStatus) hiển thị trạng thái API (xanh/đỏ)
[FEAT]   Form1.cs   — Tách TryPostAsync() riêng: trả về bool (true=2xx, false=lỗi)
[FEAT]   Form1.cs   — PostCuttingValidatorAsync: khi thất bại → EnqueueRecord() + badge đỏ
[FEAT]   Form1.cs   — EnqueueRecord(): ghi bản ghi thất bại vào api_retry_queue.txt (tab-delimited)
[FEAT]   Form1.cs   — StartRetryLoopAsync() + RetryQueueAsync(): 30s/lần đọc file, thử lại, xoá khi thành công
[REFACTOR] Form1.cs — PostCuttingValidatorAsync không còn static (cần truy cập _labApiStatus)
```

**Chi tiết:**
- Badge tại X=500, Y=594, W=960, H=100 — nằm bên phải nút Bắt Đầu / Cấu hình
- Màu xanh lá (`#00A000`) = gửi OK; màu đỏ cam (`OrangeRed`) = lỗi + đang chờ gửi lại
- File hàng đợi: `<AppDir>/api_retry_queue.txt`, tab-delimited, 7 cột, mỗi dòng 1 bản ghi
- Retry loop: mỗi 30 giây, không block UI, dừng khi form bị dispose
- Bản ghi được xoá khỏi file ngay khi API trả về 2xx; file bị xoá khi hàng đợi rỗng

---

### [2026-05-27] — Session: Aldila Cutting Validator API

```
[FEAT]   App.config   — Thêm StationName, AldilaCuttingApi_Url, AldilaCuttingApi_Enabled
[FEAT]   Form1.cs     — Thêm PostCuttingValidatorAsync() + EscapeJson() + static HttpClient (SSL bypass)
[FEAT]   Form1.cs     — _btnStartStop_Click: gọi API fire-and-forget sau khi xác định PASSED/FAILED
[DOCS]   CLAUDE.md    — Cập nhật active_context + CHANGELOG
```

**Chi tiết:**
- API endpoint: `https://192.168.96.10/aldila-portlet/service/savePrepregCuttingValidator` (POST JSON)
- Mapping: QR2 (cuộn Prepreg thực tế) → `prepregItemId/Name`; QR1 (phiếu cắt order) → `prepregOrderItemId/Name`
- SSL validation bị bypass vì server dùng self-signed cert — cần xoá khi cert hợp lệ
- API lỗi không làm crash app (try-catch, log ra `Debug.WriteLine`)
- Có thể tắt bằng `AldilaCuttingApi_Enabled = false` trong App.config

### [2026-05-25] — Session: ERP Config UI

```
[FEAT]   App.config            — Thêm 6 key ERP: TenantId, ClientId, ClientSecret, BaseUrl, Endpoint, Enabled
[FEAT]   FormConfig.cs/.Designer.cs — Thêm Tab "ERP Kết nối" với form OAuth2 (ClientSecret ẩn, checkbox show/hide)
[REFACTOR] FormConfig          — Chuyển sang TabControl (Tab QR Patterns + Tab ERP Kết nối)
```

**Chi tiết:**
- ERP dùng OAuth2 Client Credentials flow (Azure AD) — cần TenantId, ClientId, ClientSecret, BaseUrl
- ClientSecret lưu plain text trong App.config — chấp nhận được cho môi trường factory floor
- Nút Save lưu cả 2 tab cùng lúc

### [2026-05-25] — Session: Fix Reconnect + Config QR

```
[FIX]    PLCModbusManager.cs   — Thêm RECONNECT_COOLDOWN_MS=3000ms để tránh block vòng lặp
[FIX]    PLCModbusManager.cs   — Sửa EndConnect sau timeout (không gọi nữa, chỉ Close socket)
[FIX]    PLCModbusManager.cs   — Giảm Retries từ 3→1 và thêm WriteTimeout=1000ms
[FEAT]   App.config            — Thêm CutSheetQR_EndsWith và CutSheetQR_Contains (pipe-separated)
[FEAT]   FormConfig.cs/.Designer.cs — Form quản lý QR validation patterns (thêm/xoá/lưu)
[REFACTOR] Form1.cs            — Thay hardcode pattern bằng IsCutSheetQr() + LoadQrPatterns()
[FEAT]   Form1.cs              — Thêm nút "Config" để mở FormConfig tại runtime
```

**Chi tiết:**
- Reconnect: trước đây mỗi lần disconnect, vòng lặp 200ms gọi 2x EnsureConnection → block 6s. Nay có cooldown 3s nên block tối đa 2s một lần thử.
- QR patterns: `--` (EndsWith) và `"-` (Contains) được lưu trong App.config, user có thể chỉnh qua nút Config mà không cần sửa code.

<!-- Thêm session mới lên ĐẦU, trên dòng này -->

---

## 6. UNIT TEST GUIDELINES

### 6.1 Cấu trúc test file

```typescript
/**
 * @file        tests/unit/userService.test.ts
 * @description Unit tests cho src/services/userService.ts
 * @covers      getUser, createUser, updateUser, deleteUser
 */

import { describe, it, expect, beforeEach, vi } from 'vitest';
import { getUser } from '@/services/userService';
import { mockUser } from '../fixtures/user.fixture';

// ─── Mock external dependencies ───────────────────────────────────────────
vi.mock('@/lib/api', () => ({ fetchJson: vi.fn() }));

describe('userService', () => {

  // ─── getUser ────────────────────────────────────────────────────────────
  describe('getUser()', () => {

    beforeEach(() => {
      vi.clearAllMocks();
    });

    it('should return user when valid ID provided', async () => {
      // ARRANGE
      mockFetchJson.mockResolvedValue(mockUser);

      // ACT
      const result = await getUser('valid-id-123');

      // ASSERT
      expect(result).toEqual(mockUser);
      expect(mockFetchJson).toHaveBeenCalledWith('/users/valid-id-123');
    });

    it('should throw NotFoundError when user does not exist', async () => {
      // ARRANGE
      mockFetchJson.mockRejectedValue({ status: 404 });

      // ACT & ASSERT
      await expect(getUser('ghost-id')).rejects.toThrow('NotFoundError');
    });

    it('should return cached result on second call within staleTime', async () => { ... });

  });

});
```

### 6.2 Test Coverage Requirements

| Layer       | Minimum Coverage | Ghi chú                            |
|-------------|------------------|------------------------------------|
| services/   | 90%              | Mọi branch phải có test            |
| lib/        | 85%              | Đặc biệt error paths               |
| hooks/      | 80%              | Test với React Testing Library     |
| utils/      | 95%              | Pure functions — dễ test nhất      |
| components/ | 70%              | Tập trung vào interaction, không style |

### 6.3 Test Naming Convention

```
it('should <expected_behavior> when <condition>')
it('should throw <ErrorType> when <invalid_input>')
it('should NOT <behavior> when <constraint>')
```

### 6.4 Test Fixtures & Factories

```typescript
// tests/fixtures/user.fixture.ts
export const mockUser: User = {
  id:    'test-uuid-001',
  name:  'Test User',
  email: 'test@example.com',
  role:  'viewer',
};

// Factory cho biến thể
export const createMockUser = (overrides: Partial<User> = {}): User => ({
  ...mockUser,
  ...overrides,
});
```

### 6.5 Lệnh chạy test

```bash
# Chạy tất cả
pnpm test

# Watch mode (dev)
pnpm test:watch

# Coverage report
pnpm test:coverage

# Chỉ chạy 1 file
pnpm test src/services/userService
```

---

## 7. PERFORMANCE OPTIMIZATION RULES

### 7.1 Nguyên tắc chung

```
RULE-PERF-01: Đo trước khi tối ưu — dùng profiler, không đoán mò
RULE-PERF-02: Ghi PERF comment + ID trước khi thay đổi liên quan đến performance
RULE-PERF-03: Mỗi tối ưu phải có benchmark trước/sau trong CHANGELOG
RULE-PERF-04: Không dùng premature optimization gây giảm readability
```

### 7.2 Frontend Performance Checklist

```markdown
- [ ] Lazy load routes và heavy components (React.lazy / dynamic import)
- [ ] Memo hóa đúng chỗ: React.memo, useMemo, useCallback
      → CHỈ khi profiler xác nhận re-render thừa
- [ ] Image: dùng next/image hoặc lazy loading + srcset
- [ ] Bundle: kiểm tra với `pnpm build --analyze`
- [ ] Fonts: font-display: swap, preload critical fonts
- [ ] API calls: SWR / React Query staleTime hợp lý
- [ ] Long lists: virtualize với react-virtual nếu > 200 items
```

### 7.3 Backend / Node Performance Checklist

```markdown
- [ ] Database queries: có index trên cột WHERE / JOIN / ORDER BY
- [ ] N+1 queries: dùng DataLoader hoặc JOIN thay vì loop
- [ ] Caching: Redis cho hot data, TTL rõ ràng
- [ ] Pagination: KHÔNG dùng OFFSET lớn — dùng cursor-based
- [ ] Streams: dùng stream cho file lớn, không load vào RAM
- [ ] Connection pool: config đúng pool size theo load
```

### 7.4 Performance Budget

| Metric              | Target       | Critical Threshold |
|---------------------|--------------|--------------------|
| LCP                 | < 2.5s       | > 4s → reject PR   |
| FID / INP           | < 100ms      | > 300ms → reject   |
| CLS                 | < 0.1        | > 0.25 → reject    |
| JS Bundle (initial) | < 200KB gz   | > 400KB → review   |
| API p95 latency     | < 300ms      | > 1s → alert       |

---

## 8. HOW TO USE THIS FILE

### 8.1 Dành cho Claude (AI Assistant)

```
Khi đọc file này, Claude phải:

1. LUÔN đọc toàn bộ file trước khi viết bất kỳ dòng code nào
2. TUÂN THỦ naming convention, comment format đã định nghĩa
3. CẬP NHẬT active_context sau mỗi session
4. THÊM entry vào CHANGELOG mỗi khi sửa/thêm/xoá code đáng kể
5. THAM CHIẾU Decision Log trước khi đề xuất kiến trúc/công nghệ
6. VIẾT test cho mọi function mới theo Section 6
7. KIỂM TRA Performance Checklist khi code liên quan đến render/query
8. KHÔNG lặp lại câu hỏi đã có câu trả lời trong file này
```

### 8.2 Dành cho Developer

```bash
# Mỗi khi bắt đầu ngày làm việc
# 1. Pull code mới nhất
git pull origin main_dev

# 2. Cập nhật active_context trong CLAUDE.md nếu task thay đổi
# 3. Chạy test để đảm bảo baseline xanh
pnpm test

# Trước khi commit
# 1. Thêm CHANGELOG entry
# 2. Chạy lint + test
pnpm lint && pnpm test

# Khi tạo PR
# 1. Đảm bảo CLAUDE.md được cập nhật
# 2. Coverage không giảm so với main
```

### 8.3 Template prompt để dùng với Claude Code / Cowork

```
# Bắt đầu task mới:
"Đọc CLAUDE.md. Task: [mô tả task]. 
Các file liên quan: [list files].
Sau khi xong, cập nhật active_context và CHANGELOG."

# Debug / fix:
"Đọc CLAUDE.md section 4 (context) và file [X].
Bug: [mô tả]. Expected: [hành vi đúng].
Ghi FIX vào CHANGELOG sau khi sửa xong."

# Code review:
"Đọc CLAUDE.md coding standards.
Review file [X] theo đúng conventions đã định nghĩa.
Liệt kê vi phạm theo format: [Line] [Rule] [Gợi ý sửa]."

# Viết test:
"Đọc CLAUDE.md section 6. Viết unit test cho [function/file].
Đảm bảo cover: happy path, error cases, edge cases."
```

### 8.4 Maintenance

| Việc cần làm                        | Tần suất      | Người chịu trách nhiệm |
|-------------------------------------|---------------|------------------------|
| Cập nhật `active_context`           | Mỗi session   | Dev đang làm việc      |
| Thêm entry `CHANGELOG`              | Mỗi commit    | Dev đang làm việc      |
| Review và dọn CHANGELOG cũ         | Mỗi sprint    | Tech Lead              |
| Cập nhật ADR khi có quyết định mới  | Khi phát sinh | Người quyết định       |
| Review Performance Budget           | Mỗi release   | Tech Lead              |
| Audit test coverage                 | Mỗi sprint    | QA / Dev               |

---

> **Lưu ý:** File này là nguồn sự thật duy nhất (*single source of truth*) cho AI assistant làm việc với project.  
> Khi có mâu thuẫn giữa code và CLAUDE.md → **ưu tiên CLAUDE.md**, sau đó sửa code cho nhất quán.

---
*CLAUDE.md · Generated by Claude Sonnet 4.6 · Phiên bản template: 1.0.0*
