export class LeaveCount{
    userName:string;
    sickLeave:number;
    casualLeave:number;
    earnedLeave:number;
}

export class LeaveForm{
    userName:string;
    leaveFrom:Date;
    leaveTo:Date;
    reason:string;
    flag: number;
    leaveType:string;
}