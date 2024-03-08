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

export class LeaveFormAPI{
    userName:string;
    leaveFrom:string;
    leaveTo:string;
    reason:string;
    flag: number;
    leaveType:number;
}

export class LeaveApp{
    userName:string;
    leaveFrom:string;
    leaveTo:string;
    leaveType:number;
    reason:string;
}
