USE [UtilityBillingLocal]
GO
SET IDENTITY_INSERT [ref].[DomainLookup] ON 

GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (1, N'DOCUMENT_TYPE', N'BILL', N'Bill')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (2, N'DOCUMENT_TYPE', N'REMIND', N'Reminder')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (3, N'UTILITY_ACCOUNT_SOURCE', N' WSS', N'Pending Verification')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (4, N'UTILITY_ACCOUNT_SOURCE', N'CCB', N'Verified')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (5, N'DOCUMENT_STATUS', N'HLD', N'Held')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (6, N'DOCUMENT_STATUS', N'PUB', N'Published')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (7, N'DOCUMENT_STATUS', N'REL', N'Released')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (8, N'DOCUMENT_STATUS', N'REM', N'Removed')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (9, N'DOCUMENT_STATUS', N'REJ', N'Rejected')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (10, N'DOC1_TO_WSS_DOCTYPE_MAP', N'UTILITY BILL', N'BILL')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (11, N'DOC1_TO_WSS_DOCTYPE_MAP', N'REMINDERNTCE', N'REMIND')
GO
SET IDENTITY_INSERT [ref].[DomainLookup] OFF
GO
