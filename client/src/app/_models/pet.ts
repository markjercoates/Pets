export interface Pet {
    id: number;
    name: string;
    petTypeId: number;
    petTypeName: string;
    missingSince: Date;
    ownerName: string;
    ownerEmail: string;
    createdDate: Date;  
    description:string;
    microChipId:string;         
}