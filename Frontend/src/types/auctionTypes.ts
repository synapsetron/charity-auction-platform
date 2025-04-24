import { BidInfoDTO } from "./bidTypes";

export interface CreateAuctionRequestDTO {
    title: string;
    description?: string;
    startingPrice: number;
    imageUrl?: string;
    startTime: Date;
    endTime: Date;
}

export interface UpdateAuctionRequestDTO {
    id: string;
    title: string;
    description?: string;
    startingPrice: number;
    imageUrl?: string;
    startTime: Date;
    endTime: Date;
    isActive: boolean;
}

export interface AuctionResponseDTO {
    id: string;
    title: string;
    description?: string;
    startingPrice: number;
    imageUrl: string;
    startTime: Date;
    endTime: Date;
    isActive: boolean;
    organizerId: string;
    createdAt: Date;
}

export interface AuctionResponseWithBidsDTO extends AuctionResponseDTO {
    bids: BidInfoDTO[];
}
