export interface MonthlyBidStatDTO {
    month: string;
    count: number;
  }
  
  export interface PopularAuctionStatDTO {
    auctionId: string;
    auctionName: string;
    bidCount: number;
  }
  
  export interface TopUserStatDTO {
    userId: string;
    userName: string;
    totalBids: number;
  }
  
  export interface StatsOverviewDTO {
    role: string;
    myWins?: number;
    myAuctions?: number;
    balance?: number;
    userCount?: number;
    auctionCount?: number;
    activeAuctionCount?: number;
    endedAuctionCount?: number;
    totalBidAmount?: number;
    avgBidAmount?: number;
    donationCount?: number;
    monthlyBids?: MonthlyBidStatDTO[];
    topAuctions?: PopularAuctionStatDTO[];
    topUsers?: TopUserStatDTO[];
  }
  