namespace Scheduler;

public record JobSchedule(Type JobType, string CronExpression) { }
