// using UnityEngine;
// using System.Collections.Generic;

// public enum AffectedPrice
// {
//     AskingPrice = 0,     // 최초 제시가
//     PurchasePrice = 1,   // 구매가
//     AppraisedPrice = 2,  // 감정가
//     SellingPrice = 3     // 최종 판매가
// }

// public enum ItemState
// {
//     Created = 0,         // 생성됨
//     OnDisplay = 1,       // 전시 중
//     UnderRestoration = 2,// 복원 중
//     OnAuction = 3,       // 경매 중
//     Sold = 4             // 판매됨
// }

// public enum Grade
// {
//     Common = 0,      // 일반
//     Rare = 1,        // 레어
//     Unique = 2,      // 유니크
//     Legendary = 3    // 레전더리
// }

// // ========== 기본 응답 래퍼 ==========
// [System.Serializable]
// public class ApiResponse<T>
// {
//     public bool success;
//     public T data;
//     public string message;
// }

// // ========== 게임 세션 데이터 ==========
// [System.Serializable]
// public class GameSessionData // 게임 한 판 데이터
// {
//     public string sessionToken;      // 세션 토큰
//     public string playerId;          // 플레이어 ID
//     public int dayCount;             // 게임 진행 일수
//     public int money;                // 잔금
//     public int personalDebt;         // 남은 개인 빚
//     public int pawnshopDebt;         // 남은 전당포 빚
//     public int unlockedShowcaseCount;// 해금된 쇼케이스 수
//     public string nickname;          // 플레이어 닉네임
//     public string shopName;          // 상점 이름
//     public int gameEndDayCount;      // 게임 종료 시 일수
//     public string gameEndDate;       // 게임 종료 시 날짜
// }

// // ========== 마스터 데이터 (정적 - 한 번만 로드) ==========
// [System.Serializable]
// public class ItemCatalogData
// {
//     public int catalogKey;           // 카탈로그 키
//     public string name;              // 아이템 이름
//     public string imgId;             // 이미지 ID (ITM00001~ITM99999)
//     public string categoryName;      // 카테고리 이름
//     public int basePrice;            // 기본 가격
// }

// [System.Serializable]
// public class CustomerCatalogData
// {
//     public int customerKey;          // 고객 키
//     public string name;              // 고객 이름 (3글자)
//     public string favoriteCategoryName; // 선호 카테고리
//     public string imgId;             // 이미지 ID (CIM00001~CIM99999)
//     public float fraud;              // 사기꾼 기질 (0.0~1.0)
//     public float wellCollect;        // 수집력 (0.0~1.0)
//     public float clumsy;             // 서투름 (0.0~1.0)
// }

// // ========== 동적 데이터 (게임 진행 중 변경됨) ==========
// [System.Serializable]
// public class ExistingItemData
// {
//     public int itemKey;              // 아이템 키 (구분자)
//     public int catalogKey;           // 카탈로그 참조
//     public int grade;                // 진짜 등급 (히든)
//     public int foundGrade;           // 발견된 등급
//     public int flawEa;               // 진짜 흠 개수
//     public int foundFlawEa;          // 발견된 흠 개수
//     public float suspiciousFlawAura; // 수상한 흠 기운 (0.0~1.0)
//     public bool authenticity;        // 진위 여부
//     public bool isAuthenticityFound; // 진위 발견 여부
//     public int itemState;            // 아이템 상태 (ItemState enum)
// }

// [System.Serializable]
// public class DealData
// {
//     public int drcKey;               // 거래 키 (구분자)
//     public int itemKey;              // 아이템 참조
//     public int sellerKey;            // 판매자 참조
//     public int? buyerKey;            // 구매자 참조 (nullable)
//     public int? askingPrice;         // 최초 제시가
//     public int? purchasePrice;       // 구매가
//     public int? appraisedPrice;      // 감정가
//     public int? sellingPrice;        // 최종 판매가
//     public int? boughtDate;          // 구매한 게임 내 일수
//     public int? soldDate;            // 판매한 게임 내 일수
// }

// [System.Serializable]
// public class CustomerDiscoveredData
// {
//     public int customerKey;          // 고객 키 (구분자)
//     public bool isFraudDiscovered;   // 사기꾼 기질 발견 여부
//     public bool isWellCollectDiscovered; // 수집력 발견 여부
//     public bool isClumsyDiscovered;  // 서투름 발견 여부
// }

// [System.Serializable]
// public class DisplayPositionData
// {
//     public int position;             // 전시 위치 (0~7)
//     public int dealKey;              // 거래 참조
// }

// [System.Serializable]
// public class NewsData
// {
//     public string description;       // 뉴스 설명
//     public int affectedPrice;        // 영향받는 가격 타입 (AffectedPrice enum)
//     public string categoryName;      // 영향받는 카테고리
//     public int amount;               // 영향 정도 (%, +30 또는 -70 같은 형태)
// }

// // ========== 복합 응답 데이터 (정규화된 구조) ==========
// [System.Serializable]
// public class NormalizedGameData
// {
//     public GameSessionData gameSession;
//     public Dictionary<string, ExistingItemData> items;
//     public Dictionary<string, DealData> deals;
//     public Dictionary<string, CustomerDiscoveredData> discoveredCustomers;
//     public List<DisplayPositionData> displays;
//     public List<NewsData> news;
// }

// // ========== 마스터 데이터 응답 (초기 로드용) ==========
// [System.Serializable]
// public class MasterData
// {
//     public Dictionary<string, ItemCatalogData> itemCatalogs;
//     public Dictionary<string, CustomerCatalogData> customerCatalogs;
// }

// // ========== 전시장 조회 응답 ==========
// [System.Serializable]
// public class DisplayResponse
// {
//     public List<DisplayPositionData> displays;
//     public Dictionary<string, DealData> deals;
//     public Dictionary<string, ExistingItemData> items;
//     public Dictionary<string, CustomerDiscoveredData> discoveredCustomers;
// }

// // ========== 요청용 데이터 ==========
// [System.Serializable]
// public class DealStartRequest
// {
//     public string sessionToken;
//     public int sellerKey;
//     public int itemCatalogKey;
//     public int askingPrice;
//     // Optional: 아이템 히든 속성 (테스트용)
//     public int? grade;
//     public int? flawEa;
//     public bool? authenticity;
// }

// [System.Serializable]
// public class DealCompleteRequest
// {
//     public string sessionToken;
//     public int drcKey;
//     public int buyerKey;
//     public int purchasePrice;
// }

// [System.Serializable]
// public class ItemStateUpdateRequest
// {
//     public string sessionToken;
//     public int itemKey;
//     public int newState;
//     public int? restoredFlawCount;      // 복원 시
//     public int? auctionExpectedPrice;   // 경매 시
// }

// [System.Serializable]
// public class DisplayUpdateRequest
// {
//     public string sessionToken;
//     public List<DisplayUpdateItem> items;
// }

// [System.Serializable]
// public class DisplayUpdateItem
// {
//     public int position;
//     public int? itemKey;  // null이면 해당 위치 비우기
// }

// [System.Serializable]
// public class ItemActionRequest
// {
//     public string sessionToken;
//     public int itemKey;
//     public string actionType;  // "find_flaw" | "auth_check" | "appraise"
//     public int? actionLevel;   // 흠 찾기 레벨 등
//     public int? cost;          // 비용
// }

// [System.Serializable]
// public class LoanUpdateRequest
// {
//     public string sessionToken;
//     public string debtType;    // "PERSONAL" | "PAWNSHOP"
//     public int amount;         // 양수: 대출, 음수: 상환
// }

// // ========== 특수 응답 데이터 ==========
// [System.Serializable]
// public class DayNextResponse
// {
//     public GameSessionData gameSession;
//     public TransactionSummary summary;
// }

// [System.Serializable]
// public class TransactionSummary
// {
//     public int boughtSum;
//     public int soldSum;
//     public int interest;
//     public int finalMoney;
// }

// [System.Serializable]
// public class WorldRecord
// {
//     public string playerId;
//     public int clearDayCount;
//     public string clearDate;
//     public int largestProfitDealKey;
//     public int totalProfit;
// }

// // ========== 유틸리티 헬퍼 클래스 ==========
// public static class GameDataHelper
// {
//     // Dictionary에서 안전하게 가져오기
//     public static T GetOrDefault<T>(this Dictionary<string, T> dict, int key) where T : class
//     {
//         return dict.TryGetValue(key.ToString(), out T value) ? value : null;
//     }
    
//     public static T GetOrDefault<T>(this Dictionary<string, T> dict, string key) where T : class
//     {
//         return dict.TryGetValue(key, out T value) ? value : null;
//     }
// }