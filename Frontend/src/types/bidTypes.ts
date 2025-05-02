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
  }
  