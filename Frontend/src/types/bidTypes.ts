export interface BidInfoDTO {
    id: string;
    amount: number;
    auctionId: string;
    userId: string;
    userName: string;
    createdAt: string;
    isDonated: boolean;
  }
  
  export interface BidResponseWithWinnerDTO {
    id: string;
    amount: number;
    auctionId: string;
    auctionName: string;
    userId: string;
    userName: string;
    createdAt: string;
    isDonated: boolean;
    isWinner: boolean;
    isAuctionActive: boolean;
    isAuctionSold: boolean;
  }
  
  export interface BidResponseDTO {
    id: string;
    amount: number;
    auctionId: string;
    userId: string;
    userName: string;
    createdAt: string;
    isDonated: boolean;
    auctionName: string;
  }
  
  export interface CreateBidRequestDTO {
    auctionId: string;
    amount: number;
  }
  