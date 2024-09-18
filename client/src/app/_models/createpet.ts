export interface CreatePet {
  name: string;
  description: string;
  microChipId: string;
  missingSince: Date;
  ownerName: string;
  ownerEmail: string;
  petTypeId: number;
}
