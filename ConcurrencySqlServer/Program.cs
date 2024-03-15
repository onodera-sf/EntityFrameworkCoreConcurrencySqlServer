using ConcurrencySqlServer.Models.Database;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

Console.WriteLine("Hello, World!");

// 2人が別々にデータベースにアクセスする想定で２つのデータベースコンテキストを作成
Console.WriteLine("データベースコンテキストを作成します。");
using var dbContextA = new TestDatabaseDbContext();
using var dbContextB = new TestDatabaseDbContext();
using var dbContextC = new TestDatabaseDbContext();
Console.WriteLine("データベースコンテキストを作成しました。");
Console.WriteLine("");

// それぞれがデータを編集しようと読み込む
Console.WriteLine("ID:1 の Book を読み込みます。");
var bookA = dbContextA.Book.First(x => x.ID == 1);
var bookB = dbContextB.Book.First(x => x.ID == 1);
Console.WriteLine("ID:1 の Book を読み込みました。");
Console.WriteLine($"A  Book : {JsonSerializer.Serialize(bookA)}");
Console.WriteLine($"B  Book : {JsonSerializer.Serialize(bookB)}");
Console.WriteLine($"DB Book : {JsonSerializer.Serialize(dbContextC.Book.AsNoTracking().First(x => x.ID == 1))}");
Console.WriteLine("");


// A の人が最初にデータベースに更新する
bookA.Price += 500;
UpdateToDatabase(dbContextA, "A");

Console.WriteLine($"A  Book : {JsonSerializer.Serialize(bookA)}");
Console.WriteLine($"B  Book : {JsonSerializer.Serialize(bookB)}");
Console.WriteLine($"DB Book : {JsonSerializer.Serialize(dbContextC.Book.AsNoTracking().First(x => x.ID == 1))}");
Console.WriteLine("");

// そのあと B の人が最初にデータベースに更新する
bookB.Price += 300;
UpdateToDatabase(dbContextB, "B");

Console.WriteLine($"A  Book : {JsonSerializer.Serialize(bookA)}");
Console.WriteLine($"B  Book : {JsonSerializer.Serialize(bookB)}");
Console.WriteLine($"DB Book : {JsonSerializer.Serialize(dbContextC.Book.AsNoTracking().First(x => x.ID == 1))}");
Console.WriteLine("");

Console.WriteLine("処理を終了します。");



// データベースに更新するメソッド
void UpdateToDatabase(TestDatabaseDbContext dbContext, string updateUser)
{
  try
  {
    Console.WriteLine($"{updateUser} のデータベースコンテキストを変更保存します。");
    dbContext.SaveChanges();
    Console.WriteLine($"{updateUser} のデータベースコンテキストを変更保存しました。");
  }
  catch (DbUpdateConcurrencyException ex)
  {
    Console.WriteLine($"{updateUser} の更新でエラーが発生しました。");
    Console.WriteLine(ex.Message);
  }
}

