USE ngcolombia
GO

DECLARE @EventId UNIQUEIDENTIFIER = NEWID();

INSERT INTO Events ([Id], [Name], [Description], [ConfirmationDateLimit])
SELECT @EventId, 'NG-Colombia', 'COLOMBIA''S FIRST ANGULAR CONFERENCE', '2017-11-18'


INSERT INTO TicketTypes([eventId], [Code], [Name], [Value], [TicketQuantity])
SELECT @EventId, 'main-conference', 'Main Conference', 25, 400 UNION ALL
SELECT @EventId, 'workshop-1', 'Workshop # 1', 20, 200 UNION ALL
SELECT @EventId, 'workshop-2', 'Workshop # 2', 20, 200

go