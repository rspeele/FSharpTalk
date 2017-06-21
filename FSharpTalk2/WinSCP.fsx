open System
open System.IO

type FTPEncryption =
    | ImplicitTLSEncryption
    | ExplcitTLSEncryption

type Protocol =
    | SCP
    | FTP of FTPEncryption option

type ConnectionParameters =
    {
        Protocol : Protocol
        HostName : string
        PortNumber : int
        UserName : string
        Password : string
    }

type DirectoryEntryType =
    | File
    | Directory

type DirectoryEntry =
    {   
        Type : DirectoryEntryType
        Name : string
        Size : int64
        Modified : DateTime
        Created : DateTime
    }

type IConnection =
    inherit IDisposable
    abstract member CurrentDirectory : string
    abstract member ChangeDirectory : string -> unit
    abstract member ListDirectoryEntries : unit -> DirectoryEntry list
    abstract member OpenFile : string -> Stream

// glue code to get a connection from the user input
type IConnectionFactory =
    abstract member GetConnection : ConnectionParameters -> IConnection