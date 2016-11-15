USE [WssApplicationLocal]
GO
SET IDENTITY_INSERT [ref].[DomainLookup] ON 

GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (1, N'SUBSCRIBE_TX_STATUS', N'PENDING', N'Pending')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (2, N'SUBSCRIBE_TX_STATUS', N'HOLD', N'Held')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (3, N'SUBSCRIBE_TX_STATUS', N'PROCESSED', N'Processed')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (4, N'SUBSCRIBE_TX_TYPE', N'SUB', N'Subscribed')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (5, N'SUBSCRIBE_TX_TYPE', N'UNSUB', N'Unsubscribed')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (6, N'WSS_ACCOUNT_STATUS', N'REG', N'Registered')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (7, N'WSS_ACCOUNT_STATUS', N'ACT', N'Active')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (8, N'WSS_ACCOUNT_STATUS', N'UNSUB', N'Unsubscribed')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (9, N'WSS_ACCOUNT_STATUS', N'LCKD', N'Locked')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (10, N'AUDIT_SUBJECT', N'WSS_ACCT', N'WSS Account')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (11, N'AUDIT_SUBJECT', N'WSS_INTBILL', N'WSS Intercepted Bill')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (12, N'AUDIT_EVENT_WSS_ACCT', N'CHNGUID', N'Change User Id')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (13, N'AUDIT_EVENT_WSS_ACCT', N'UNSUB', N'Unsubscribe')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (14, N'AUDIT_EVENT_WSS_ACCT', N'ACCTLOCK', N'Account Unlocked')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (15, N'AUDIT_EVENT_WSS_ACCT', N'RSNDACT', N'Resend Activation')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (16, N'AUDIT_EVENT_WSS_ACCT', N'REG', N'Register')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (17, N'AUDIT_EVENT_WSS_ACCT', N'RSTPWD', N'Changed Password')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (18, N'AUDIT_EVENT_WSS_ACCT', N'LNKACCT', N'Linked Account')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (19, N'AUDIT_EVENT_WSS_ACCT', N'ULINKACCT', N'Unlinked Account')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (20, N'AUDIT_EVENT_WSS_ACCT', N'CHNGNN', N'Nickname Changed')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (21, N'AUDIT_EVENT_WSS_ACCT', N'ADDEMAIL', N'Added Notification only User')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (22, N'AUDIT_EVENT_WSS_ACCT', N'REMEMAIL', N'Remove Notification only User')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (23, N'AUDIT_EVENT_WSS_ACCT', N'CHSECQ', N'Security Question Changed')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (24, N'AUDIT_EVENT_WSS_ACCT', N'RSTPWDEMAIL', N'Reset Password')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (25, N'AUDIT_EVENT_WSS_ACCT', N'ACTVTN', N'Activate Profile')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (26, N'AUDIT_EVENT_WSS_ACCT', N'ACCTLOCKD', N'Account Locked')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (27, N'AUDIT_EVENT_WSS_ACCT', N'LGOUT', N'Logout')
GO
INSERT [ref].[DomainLookup] ([DomainLookupId], [DomainName], [LookupCode], [LookupValue]) VALUES (28, N'AUDIT_EVENT_WSS_ACCT', N'OPTOUT', N'Optout')
GO
SET IDENTITY_INSERT [ref].[DomainLookup] OFF
GO
