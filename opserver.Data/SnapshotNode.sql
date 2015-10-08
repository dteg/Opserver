CREATE TABLE [dbo].[SnapshotNode]
(
	[SnapshotCounter] INT IDENTITY(1,1) NOT NULL,
	[SnapshotID] INT NOT NULL , 
	[NodeID] INT NOT NULL, 
	[Date] DATETIME NOT NULL, 
	CONSTRAINT [FK_SnapshotNode_Snapshot] FOREIGN KEY (SnapshotID) REFERENCES Snapshot(SnapshotID), 
	CONSTRAINT [FK_SnapshotNode_Node] FOREIGN KEY (NodeID) REFERENCES Node(NodeID), 
    CONSTRAINT [PK_SnapshotNode] PRIMARY KEY ([SnapshotCounter])
)
