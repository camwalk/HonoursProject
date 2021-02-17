export interface DirectMessage {
    id: number;
    senderId: number;
    senderUsername: string;
    senderPhotoUrl: string;
    recieverId: number;
    recieverUsername: string;
    recieverPhotoUrl: string;
    messageContent: string;
    timeRead?: Date;
    timeSent: Date;
}