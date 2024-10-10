INSERT INTO [dbo].[Ponds] ([Name], [Image], [Size], [Depth], [Volume], [DrainCount], [PumpCapacity], [Description], [CreatedAt], [UpdatedAt])
VALUES
('Pond A', 'pondA.jpg', 500, 2.5, 1250, 2, 50, 'Main Pond', GETDATE(), GETDATE()),
('Pond B', 'pondB.jpg', 300, 2, 600, 1, 30, 'Secondary Pond', GETDATE(), GETDATE()),
('Pond C', 'pondC.jpg', 700, 2.8, 1960, 3, 60, 'Large Pond', GETDATE(), GETDATE()),
('Pond D', 'pondD.jpg', 400, 2.3, 920, 2, 40, 'Medium Pond', GETDATE(), GETDATE()),
('Pond E', 'pondE.jpg', 250, 1.8, 450, 1, 25, 'Small Pond', GETDATE(), GETDATE());


INSERT INTO [dbo].[KoiFish] ([Name], [Image], [BodyShape], [Age], [Size], [Weight], [Gender], [Breed], [Origin], [Price], [PondID], [CreatedAt], [UpdatedAt])
VALUES
('Koi 1', 'koi1.jpg', 'Long', 2, 30, 5.5, 'Male', 'Kohaku', 'Japan', 1500.00, 1, GETDATE(), GETDATE()),
('Koi 2', 'koi2.jpg', 'Round', 3, 35, 6.0, 'Female', 'Sanke', 'China', 1200.00, 2, GETDATE(), GETDATE()),
('Koi 3', 'koi3.jpg', 'Slim', 1, 20, 4.0, 'Male', 'Showa', 'Japan', 1300.00, 3, GETDATE(), GETDATE()),
('Koi 4', 'koi4.jpg', 'Round', 4, 40, 7.5, 'Female', 'Utsurimono', 'Vietnam', 2000.00, 4, GETDATE(), GETDATE()),
('Koi 5', 'koi5.jpg', 'Long', 5, 50, 8.0, 'Male', 'Shusui', 'Indonesia', 1800.00, 5, GETDATE(), GETDATE());

INSERT INTO [dbo].[FoodRequirements] ([KoiID], [DevelopmentStage], [RequiredAmount], [Notes], [CreatedAt])
VALUES
(1, 'Juvenile', 3.5, 'High protein requirement', GETDATE()),
(2, 'Adult', 2.8, 'Balanced diet', GETDATE()),
(3, 'Young', 4.0, 'Fast growth, needs extra nutrition', GETDATE()),
(4, 'Adult', 2.5, 'Maintain size and weight', GETDATE()),
(5, 'Senior', 2.0, 'Low metabolism, less food', GETDATE());


INSERT INTO [dbo].[KoiGrowth] ([KoiID], [DateMeasured], [Size], [Weight], [Notes], [CreatedAt])
VALUES
(1, '2024-09-01', 30, 5.5, 'Healthy growth', GETDATE()),
(2, '2024-09-05', 35, 6.0, 'Slightly overweight', GETDATE()),
(3, '2024-09-10', 20, 4.0, 'Normal growth', GETDATE()),
(4, '2024-09-15', 40, 7.5, 'Excellent condition', GETDATE()),
(5, '2024-09-20', 50, 8.0, 'Senior growth stage', GETDATE());

INSERT INTO [dbo].[News] ([Title], [Content], [Author], [CreatedAt], [UpdatedAt])
VALUES
('Koi Festival 2024', 'Join us at the Koi Festival for exciting events and exhibitions.', 'Admin', GETDATE(), GETDATE()),
('New Pond Maintenance Tips', 'Learn how to keep your ponds clean and healthy for your Koi.', 'John Smith', GETDATE(), GETDATE()),
('Koi Health Insights', 'Explore the latest health tips for ensuring your Koi growth.', 'Jane Doe', GETDATE(), GETDATE()),
('Special Koi Offers', 'Get discounts on Koi purchases this month!', 'Mike Johnson', GETDATE(), GETDATE()),
('Winter Pond Preparation', 'Prepare your ponds for the winter season with these tips.', 'Koi Expert', GETDATE(), GETDATE());

INSERT INTO [dbo].[Orders] ([ProductName], [Quantity], [Price], [CreatedAt])
VALUES
('Koi Feed 1kg', 3, 200.00, GETDATE()),
('Pond Pump', 1, 1500.00, GETDATE()),
('Water Filter', 2, 600.00, GETDATE()),
('Koi Breeder Kit', 1, 3500.00, GETDATE()),
('Koi Medication', 5, 50.00, GETDATE());


INSERT INTO [dbo].[SaltRequirements] ([PondID], [RequiredSaltAmount], [Notes], [CreatedAt])
VALUES
(1, 5.0, 'Initial treatment', GETDATE()),
(2, 3.5, 'Routine maintenance', GETDATE()),
(3, 4.0, 'Post-treatment', GETDATE()),
(4, 3.0, 'Low-salt pond', GETDATE()),
(5, 6.0, 'High mineral content required', GETDATE());


INSERT INTO [dbo].[WaterParameters] ([PondID], [DateMeasured], [Temperature], [SaltLevel], [PHLevel], [O2Level], [NO2Level], [NO3Level], [PO4Level], [Recommendation], [CreatedAt])
VALUES
(1, '2024-09-01', 25.5, 0.5, 7.8, 8.0, 0.05, 0.10, 0.02, 'Maintain current parameters', GETDATE()),
(2, '2024-09-05', 24.0, 0.4, 7.5, 7.9, 0.02, 0.08, 0.01, 'Slightly increase O2 levels', GETDATE()),
(3, '2024-09-10', 26.0, 0.6, 8.0, 8.2, 0.07, 0.12, 0.03, 'Reduce NO3 levels', GETDATE()),
(4, '2024-09-15', 25.0, 0.5, 7.9, 7.8, 0.03, 0.09, 0.02, 'Monitor PO4 levels', GETDATE()),
(5, '2024-09-20', 24.5, 0.4, 7.7, 8.1, 0.04, 0.11, 0.01, 'Good balance, maintain as is', GETDATE());

