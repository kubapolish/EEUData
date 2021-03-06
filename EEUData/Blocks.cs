﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EEUniverse.Library;

namespace EEUData
{
    public enum BlockId : ushort
    {
        #region fg
        //gravity
        Empty = 0,
        GravityLeft = 13,
        GravityUp = 14,
        GravityRight = 15,
        GravityNone = 16,
        GravitySlow = 71,
        //basic
        BasicWhite = 1,
        BasicGrey = 2,
        BasicBlack = 3,
        BasicRed = 4,
        BasicOrange = 5,
        BasicYellow = 6,
        BasicGreen = 7,
        BasicCyan = 8,
        BasicBlue = 9,
        BasicPurple = 10,
        //stone
        StoneWhite = 18,
        StoneGrey = 19,
        StoneBlack = 20,
        StoneRed = 21,
        StoneOrange = 22,
        StoneYellow = 23,
        StoneGreen = 24,
        StoneCyan = 25,
        StoneBlue = 26,
        StonePurple = 27,
        //beveled
        BeveledWhite = 28,
        BeveledGrey = 29,
        BeveledBlack = 30,
        BeveledRed = 31,
        BeveledOrange = 32,
        BeveledYellow = 33,
        BeveledGreen = 34,
        BeveledCyan = 35,
        BeveledBlue = 36,
        BeveledPurple = 37,
        //metal
        MetalSilver = 38,
        MetalSteel = 39,
        MetalIron = 40,
        MetalGold = 41,
        MetalBronze = 42,
        MetalCopper = 43,
        //glass
        GlassWhite = 45,
        GlassBlack = 46,
        GlassRed = 47,
        GlassOrange = 48,
        GlassYellow = 49,
        GlassGreen = 50,
        GlassCyan = 51,
        GlassBlue = 52,
        GlassPurple = 53,
        GlassPink = 54,
        //tiles
        TilesWhite = 72,
        TilesGrey = 73,
        TilesBlack = 74,
        TilesRed = 75,
        TilesOrange = 76,
        TilesYellow = 77,
        TilesGreen = 78,
        TilesCyan = 79,
        TilesBlue = 80,
        TilesPurple = 81,
        //special
        Black = 12,
        Secret = 95,
        Clear = 96,
        //signs
        SignWood = 55,
        SignRed = 56,
        SignGreen = 57,
        SignBlue = 58,
        //coins
        CoinGold = 11,
        CoinBlue = 97,
        CoinGoldDoor = 104,
        CoinBlueDoor = 105,
        //control
        Spawn = 44,
        Godmode = 17,
        Crown = 70,
        Portal = 59,
        //actions(effects)
        EffectClear = 92,
        EffectMultiJump = 93,
        EffectHighJump = 94,
        //switches
        SwitchLocal = 98,
        SwitchLocalReset = 99,
        SwitchLocalDoor = 100,
        SwitchGlobal = 101,
        SwitchGlobalReset = 102,
        SwitchGlobalDoor = 103,
        //brick
        BrickClayRed = 107,
        BrickStoneGrey = 108,
        //platform
        Platform = 106,
        //terrain
        TerrainDirt = 111,
        TerrainSand = 112,
        //jungle
        JungleGray = 113,
        JungleRed = 114,
        JungleGreen = 115,
        JungleBlue = 116,
        JungleYellow = 117,
        JungleFace = 118,
        JunglePot = 119,
        JunglePlatform = 120,
        JungleLeavesHorizontal = 121,
        JungleLeavesVertical = 122,
        #endregion
        #region bg
        //basic bg
        BgBasicWhite = 60,
        BgBasicGrey = 61,
        BgBasicBlack = 62,
        BgBasicRed = 63,
        BgBasicOrange = 64,
        BgBasicYellow = 65,
        BgBasicGreen = 66,
        BgBasicCyan = 67,
        BgBasicBlue = 68,
        BgBasicPurple = 69,
        //tiles bg
        BgTilesWhite = 82,
        BgTilesGrey = 83,
        BgTilesBlack = 84,
        BgTilesRed = 85,
        BgTilesOrange = 86,
        BgTilesYellow = 87,
        BgTilesGreen = 88,
        BgTilesCyan = 89,
        BgTilesBlue = 90,
        BgTilesPurple = 91,
        //brick bg
        BgBrickClayRed = 109,
        BgBrickStoneGrey = 110,
        //jungle bg
        BgJungleGray = 123,
        BgJungleRed = 124,
        BgJungleGreen = 125,
        BgJungleBlue = 126,
        BgJungleYellow = 127,
        #endregion
    }

    public static partial class Extensions
    {
        public static bool IsBackground(this BlockId bid) => bid.ToString().StartsWith("Bg");
        public static bool IsForeground(this BlockId bid) => !IsBackground(bid);
    }

    public partial class RoomData
    {
        public Block this[int l, int x, int y]
        {
            get { return Blocks[l, x, y]; }
            set { Blocks[l, x, y] = value; }
        }

        public Block[,,] Blocks { get; protected set; }

        public static int GetARGBColor(ushort fg = (ushort)BlockId.Black, ushort bg = (ushort)BlockId.Black, int backgroundColor = -1) => FromBlockColorToARGB(GetBlockColor(fg, bg, backgroundColor));
        public static int GetBlockColor(ushort fg = (ushort)BlockId.Black, ushort bg = (ushort)BlockId.Black, int backgroundColor = -1)
        {
            unchecked
            {
                var ct = BlockColors;
                const int BLACK = 0;
                const int TRANSPARENT = -2;
                int c = BLACK;
                int n = ct[fg];
                if (n == -1) n = ct[bg];
                if (n == -1 && bg != 0) n = -2;
                if (n == -2) c = TRANSPARENT;
                if (n == -1) c = backgroundColor != -1 ? backgroundColor : BLACK;
                if (n >= 0) c = n;
                return c;
            }
        }
        public virtual int GetARGBColor(int x, int y, int layer = -1, int backgroundColor = -2) => FromBlockColorToARGB(GetBlockColor(x, y, layer, backgroundColor));
        public virtual int GetBlockColor(int x, int y, int layer = -1, int backgroundColor = -2)
        {
            if (backgroundColor == -2) backgroundColor = this.BackgroundColor;
            if (layer < -1 && layer > 1) throw new ArgumentOutOfRangeException(nameof(layer));
            if (layer == -1)
                return GetBlockColor((ushort)this.Blocks[1, x, y].Id, (ushort)this.Blocks[0, x, y].Id, backgroundColor);
            else if (layer == 0)
                return GetBlockColor((ushort)BlockId.Empty, (ushort)this.Blocks[0, x, y].Id, backgroundColor);
            else if (layer == 1)
                return GetBlockColor((ushort)this.Blocks[1, x, y].Id, (ushort)BlockId.Empty, backgroundColor);
            else throw new ArgumentOutOfRangeException(nameof(layer));
        }

        /// <summary>
        /// -2 = invisible like black block
        /// -1 = transparent like coin
        /// </summary>
        public static readonly IReadOnlyDictionary<ushort, int> BlockColors = new Dictionary<ushort, int>()
        {
            //gravity
            { (ushort)BlockId.Empty, -1 },
            { (ushort)BlockId.GravityLeft, -1 },
            { (ushort)BlockId.GravityUp, -1 },
            { (ushort)BlockId.GravityRight, -1 },
            { (ushort)BlockId.GravityNone, -1 },
            { (ushort)BlockId.GravitySlow, -1 },
            //basic
            { (ushort)BlockId.BasicWhite, 11842740 },
            { (ushort)BlockId.BasicGrey, 7368816 },
            { (ushort)BlockId.BasicBlack, 3421236 },
            { (ushort)BlockId.BasicRed, 11678012 },
            { (ushort)BlockId.BasicOrange, 12216104 },
            { (ushort)BlockId.BasicYellow, 11641905 },
            { (ushort)BlockId.BasicGreen, 3975215 },
            { (ushort)BlockId.BasicCyan, 3775669 },
            { (ushort)BlockId.BasicBlue, 3363761 },
            { (ushort)BlockId.BasicPurple, 10171570 },
            //stone
            { (ushort)BlockId.StoneWhite, 10131601 },
            { (ushort)BlockId.StoneGrey, 5789779 },
            { (ushort)BlockId.StoneBlack, 3157805 },
            { (ushort)BlockId.StoneRed, 7678252 },
            { (ushort)BlockId.StoneOrange, 8538912 },
            { (ushort)BlockId.StoneYellow, 7498280 },
            { (ushort)BlockId.StoneGreen, 4224037 },
            { (ushort)BlockId.StoneCyan, 2914915 },
            { (ushort)BlockId.StoneBlue, 3096436 },
            { (ushort)BlockId.StonePurple, 5515637 },
            //beveled
            { (ushort)BlockId.BeveledWhite, 10921638 },
            { (ushort)BlockId.BeveledGrey, 6908265 },
            { (ushort)BlockId.BeveledBlack, 3815994 },
            { (ushort)BlockId.BeveledRed, 11941933 },
            { (ushort)BlockId.BeveledOrange, 11954222 },
            { (ushort)BlockId.BeveledYellow, 11902507 },
            { (ushort)BlockId.BeveledGreen, 3257164 },
            { (ushort)BlockId.BeveledCyan, 3252917 },
            { (ushort)BlockId.BeveledBlue, 3494586 },
            { (ushort)BlockId.BeveledPurple, 11481269 },
            //metal
            { (ushort)BlockId.MetalSilver, 12303292 },
            { (ushort)BlockId.MetalSteel, 9211279 },
            { (ushort)BlockId.MetalIron, 5658714 },
            { (ushort)BlockId.MetalGold, 14529619 },
            { (ushort)BlockId.MetalBronze, 14454605 },
            { (ushort)BlockId.MetalCopper, 13134671 },
            //glass
            { (ushort)BlockId.GlassWhite, 14211288 },
            { (ushort)BlockId.GlassBlack, 5329233 },
            { (ushort)BlockId.GlassRed, 15305358 },
            { (ushort)BlockId.GlassOrange, 15250569 },
            { (ushort)BlockId.GlassYellow, 15258761 },
            { (ushort)BlockId.GlassGreen, 9037973 },
            { (ushort)BlockId.GlassCyan, 9096681 },
            { (ushort)BlockId.GlassBlue, 8687847 },
            { (ushort)BlockId.GlassPurple, 11897320 },
            { (ushort)BlockId.GlassPink, 15043305 },
            //tiles
            { (ushort)BlockId.TilesWhite, 12170655 },
            { (ushort)BlockId.TilesGrey, 9605517 },
            { (ushort)BlockId.TilesBlack, 6908004 },
            { (ushort)BlockId.TilesRed, 11431789 },
            { (ushort)BlockId.TilesOrange, 11437421 },
            { (ushort)BlockId.TilesYellow, 11443054 },
            { (ushort)BlockId.TilesGreen, 8365941 },
            { (ushort)BlockId.TilesCyan, 7906971 },
            { (ushort)BlockId.TilesBlue, 7834277 },
            { (ushort)BlockId.TilesPurple, 9206184 },
            //bg basic
            { (ushort)BlockId.BgBasicWhite, 7566195},
            { (ushort)BlockId.BgBasicGrey, 4210752 },
            { (ushort)BlockId.BgBasicBlack, 657930 },
            { (ushort)BlockId.BgBasicRed, 6691364 },
            { (ushort)BlockId.BgBasicOrange, 6696474 },
            { (ushort)BlockId.BgBasicYellow, 6706202 },
            { (ushort)BlockId.BgBasicGreen, 2909722 },
            { (ushort)BlockId.BgBasicCyan, 1728102 },
            { (ushort)BlockId.BgBasicBlue, 1718374 },
            { (ushort)BlockId.BgBasicPurple, 5184102 },
            //bg tiles
            { (ushort)BlockId.BgTilesWhite, 6578517 },
            { (ushort)BlockId.BgTilesGrey, 4605510 },
            { (ushort)BlockId.BgTilesBlack, 2763306 },
            { (ushort)BlockId.BgTilesRed, 5123885 },
            { (ushort)BlockId.BgTilesOrange, 5127469 },
            { (ushort)BlockId.BgTilesYellow, 5130285 },
            { (ushort)BlockId.BgTilesGreen, 3361586 },
            { (ushort)BlockId.BgTilesCyan, 2969165 },
            { (ushort)BlockId.BgTilesBlue, 2965071 },
            { (ushort)BlockId.BgTilesPurple, 4339544 },
            //specials
            { (ushort)BlockId.Black, -2 },
            { (ushort)BlockId.Secret, -2 },
            { (ushort)BlockId.Clear, -1 },
            //signs
            { (ushort)BlockId.SignWood, -1 },
            { (ushort)BlockId.SignRed, -1 },
            { (ushort)BlockId.SignGreen, -1 },
            { (ushort)BlockId.SignBlue, -1 },
            //coins
            { (ushort)BlockId.CoinGold, -1 },
            { (ushort)BlockId.CoinBlue, -1 },
            { (ushort)BlockId.CoinGoldDoor, -1 },
            { (ushort)BlockId.CoinBlueDoor, -1 },
            //actions
            { (ushort)BlockId.Spawn, -1 },
            { (ushort)BlockId.Godmode, -1 },
            { (ushort)BlockId.Crown, -1 },
            { (ushort)BlockId.Portal, -1 },
            //effects
            { (ushort)BlockId.EffectClear, -1 },
            { (ushort)BlockId.EffectMultiJump, -1 },
            { (ushort)BlockId.EffectHighJump, -1 },
            //switches
            { (ushort)BlockId.SwitchLocal, -1 },
            { (ushort)BlockId.SwitchLocalReset, -1 },
            { (ushort)BlockId.SwitchLocalDoor, -1 },
            { (ushort)BlockId.SwitchGlobal, -1 },
            { (ushort)BlockId.SwitchGlobalReset, -1 },
            { (ushort)BlockId.SwitchGlobalDoor, -1 },
            //bg brick
            { (ushort)BlockId.BrickClayRed, 10638660 },
            { (ushort)BlockId.BrickStoneGrey, 8093055 },
            { (ushort)BlockId.BgBrickClayRed, 7617327 },
            { (ushort)BlockId.BgBrickStoneGrey, 4671818 },
            //platform
            { (ushort)BlockId.Platform, -1 },
            //terrain
            { (ushort)BlockId.TerrainDirt, 8020293 },
            { (ushort)BlockId.TerrainSand, 14861457 },
            //jungle
            { (ushort)BlockId.JungleGray, 7829862 },
            { (ushort)BlockId.JungleRed, 10840673 },
            { (ushort)BlockId.JungleGreen, 6914876 },
            { (ushort)BlockId.JungleBlue, 7440788 },
            { (ushort)BlockId.JungleYellow, 8421949 },
            { (ushort)BlockId.JungleFace, -1 },
            { (ushort)BlockId.JunglePot, -1 },
            { (ushort)BlockId.JunglePlatform, -1 },
            { (ushort)BlockId.JungleLeavesHorizontal, -1 },
            { (ushort)BlockId.JungleLeavesVertical, -1 },
            { (ushort)BlockId.BgJungleGray, 5592909 },
            { (ushort)BlockId.BgJungleRed, 6771020 },
            { (ushort)BlockId.BgJungleGreen, 5331773 },
            { (ushort)BlockId.BgJungleBlue, 5529183 },
            { (ushort)BlockId.BgJungleYellow, 5855806 },
        };
        public static uint FromBlockColorToARGB(uint blockcolor) => (uint)FromBlockColorToARGB((int)blockcolor);
        public static int FromBlockColorToARGB(int blockcolor)
        {
            unchecked
            {
                return (blockcolor != -1 && blockcolor != -2) ? ((int)0xff000000 | blockcolor) : ((blockcolor == -1) ? 0 : 1);
            }
        }
        public static uint FromARGBToBlockColor(uint argb) => (uint)FromARGBToBlockColor((int)argb);
        public static int FromARGBToBlockColor(int argb)
        {
            unchecked
            {
                if (((argb & (int)0xff000000) != (int)0xff000000) && argb != 0 && argb != 1) throw new ArgumentOutOfRangeException("bits 0xff000000 need to be set.");
                return (argb < 0 || argb > 1) ? (int)0x00ffffff & argb : argb == 0 ? -1 : -2;
            }
        }

        public static Block[,,] GetClearedWorld(int width, int height, int layers = 2)
        {
            if (layers < 2) throw new ArgumentOutOfRangeException(nameof(layers) + " must be at least 2.");

            int w = width, h = height;
            var b = new Block[layers, w, h];
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    b[1, x, y] = new Block((y == 0 || y == (h - 1)) || (x == 0 || x == (w - 1)) ? BlockId.BasicGrey : BlockId.Empty);
                }
            }
            return b;
        }

        /// <summary>
        /// Block[layer,x,y]
        /// </summary>
        public static Block[,,] DeserializeBlockData(List<object> m, int width, int height, ref int index)
        {
            var blocks = new Block[2, width, height];

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    int value = 0;
                    if (m[index++] is int iValue)
                        value = iValue;

                    var backgroundId = (ushort)(value >> 16);
                    var foregroundId = (ushort)(65535 & value);

                    blocks[0, x, y] = new Block(backgroundId, null);
                    blocks[1, x, y] = HandleBlock(foregroundId, m, ref index);
                }
            return blocks;
        }
        public static Block HandleBlock(Message m)
        {
            switch (m.Type)
            {
                case MessageType.PlaceBlock:
                    {
                        ushort id = (ushort)(int)m[4];
                        int index = 5;
                        var block = HandleBlock(id, m.Data, ref index);
                        block.PlayerId = (int)m[0];
                        return block;
                    }
                default: throw new ArgumentException($"message type {m.Type} not supported");
            }
        }
        public static Block HandleBlock(ushort id, List<object> data, ref int index)
        {
            ExtraBlockData bd = null;
            if (data != null)
                switch (id)
                {
                    case (ushort)BlockId.SignWood:
                    case (ushort)BlockId.SignRed:
                    case (ushort)BlockId.SignGreen:
                    case (ushort)BlockId.SignBlue:
                        bd = SignBlockData.Deserialize(data, ref index);
                        break;

                    case (ushort)BlockId.Portal:
                        bd = PortalBlockData.Deserialize(data, ref index);
                        break;

                    case (ushort)BlockId.EffectMultiJump:
                    case (ushort)BlockId.EffectHighJump:
                        bd = EffectBlockData.Deserialize(data, ref index);
                        break;

                    case (ushort)BlockId.SwitchLocal:
                    case (ushort)BlockId.SwitchLocalReset:
                    case (ushort)BlockId.SwitchGlobal:
                    case (ushort)BlockId.SwitchGlobalReset:
                        bd = SwitchBlockData.Deserialize(data, ref index, false);
                        break;
                    case (ushort)BlockId.SwitchGlobalDoor:
                    case (ushort)BlockId.SwitchLocalDoor:
                    case (ushort)BlockId.CoinGoldDoor:
                    case (ushort)BlockId.CoinBlueDoor:
                        bd = SwitchBlockData.Deserialize(data, ref index, true);
                        break;

                    case (ushort)BlockId.Platform:
                    case (ushort)BlockId.JunglePlatform:
                        bd = PlatformBlockData.Deserialize(data, ref index);
                        break;
                }
            return new Block(id, bd);
        }
    }

    public struct Block : IEquatable<Block>
    {
        public Block(int id = 0) : this((ushort)id, 0, null) { }
        public Block(int id = 0, int playerId = 0) : this((ushort)id, playerId, null) { }
        public Block(int id = 0, ExtraBlockData data = null) : this((ushort)id, 0, data) { }
        public Block(int id = 0, int playerId = 0, ExtraBlockData data = null) : this((ushort)id, playerId, data) { }
        public Block(BlockId id = 0) : this((ushort)id, 0, null) { }
        public Block(BlockId id = 0, int playerId = 0) : this((ushort)id, playerId, null) { }
        public Block(BlockId id = 0, ExtraBlockData data = null) : this((ushort)id, 0, data) { }
        public Block(BlockId id = 0, int playerId = 0, ExtraBlockData data = null) : this((ushort)id, playerId, data) { }
        public Block(ushort id = 0) : this(id, 0, null) { }
        public Block(ushort id = 0, int playerId = 0) : this(id, playerId, null) { }
        public Block(ushort id = 0, ExtraBlockData data = null) : this(id, 0, data) { }
        public Block(ushort id = 0, int playerId = 0, ExtraBlockData data = null)
        {
            this.Id = id;
            this.PlayerId = playerId;
            this.Data = data;
        }
        /// <summary>
        /// create block from serialized data.
        /// </summary>
        public Block(List<object> data, ref int index, int playerId = 0)
        {
            var block = Deserialize(data, ref index);
            this.Id = block.Id;
            this.PlayerId = playerId;
            this.Data = block.Data;
        }
        public Block(int id, List<object> data, ref int index, int playerId = 0) : this((ushort)id, data, ref index, playerId) { }
        public Block(ushort id, List<object> data, ref int index, int playerId = 0)
        {
            var block = Deserialize(id, data, ref index);
            this.Id = block.Id;
            this.PlayerId = playerId;
            this.Data = block.Data;
        }

        /// <summary>
        /// Id of block
        /// </summary>
        public ushort Id { get; set; }
        /// <summary>
        /// Player ID of whoever placed block. 0 if unknown.
        /// </summary>
        public int PlayerId { get; set; }
        /// <summary>
        /// Extra block data for switches, portals etc. null for regular blocks
        /// </summary>
        public ExtraBlockData Data { get; set; }

        public List<object> Serialize()
        {
            var list = new List<object>();
            list.Add(Id);
            if (Data != null) list.AddRange(Data.Serialize());
            return list;
        }
        public static Block Deserialize(int id, List<object> data, ref int index) => Deserialize((ushort)id, data, ref index);
        public static Block Deserialize(ushort id, List<object> data, ref int index) => RoomData.HandleBlock(id, data, ref index);
        /// <summary>
        /// this reads the block id from data.
        /// </summary>
        public static Block Deserialize(List<object> data, ref int index)
        {
            ushort bid;
            var id = data[index++];
            if (id is int idint) bid = (ushort)idint;
            else if (id is ushort idushort) bid = idushort;
            else throw new ArgumentException($"unable to deserialize block id (type is {id.GetType()})");

            return Deserialize(bid, data, ref index);
        }

        /// <summary>
        /// converts this block instance to human readable format.
        /// </summary>
        public override string ToString() => $"[{nameof(Id)}:({Id}:{((BlockId)Id)}), {nameof(PlayerId)}:{PlayerId}{(Data != null ? ", " + Data.ToString() : "")}]";

        public override int GetHashCode() => Id ^ PlayerId ^ (Data?.GetHashCode() ?? 0);
        public static bool operator ==(Block left, Block right) => left.Equals(right);
        public static bool operator !=(Block left, Block right) => !left.Equals(right);
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Block)) return false;
            return Equals((Block)obj);
        }
        public bool Equals(Block other) => Id == other.Id && (Data != null ? Data.Equals(other.Data) : other.Data == null);//not comparing playerid here since that's not relevant for the block itself
    }
    public abstract class ExtraBlockData : IEquatable<ExtraBlockData>
    {
        public abstract List<object> Serialize();
        /// <summary>
        /// converts this block data to human readable format.
        /// </summary>
        public abstract override string ToString();
        public abstract override int GetHashCode();
        public abstract override bool Equals(object obj);
        public abstract bool Equals(ExtraBlockData obj);
    }
    public interface IMorphable { int Morph { get; set; } }
    public interface IFlippable { bool Flipped { get; set; } }
    public class SignBlockData : ExtraBlockData, IEquatable<SignBlockData>, IMorphable
    {
        public SignBlockData(List<object> data, ref int index) => Deserialize(data, ref index);
        public SignBlockData(string text = null, int morph = 0)
        {
            this.Text = text;
            this.Morph = morph;
        }

        public string Text { get; set; }
        public int Morph { get; set; }
        int IMorphable.Morph { get => Morph; set => Morph = value; }

        public override List<object> Serialize() => new List<object>() { Text, Morph };
        public static SignBlockData Deserialize(List<object> data, ref int index) => new SignBlockData((string)data[index++], (int)data[index++]);

        public override string ToString() => $"{nameof(Text)}:\"{Text.Replace("\"", "\\\"")}\", {nameof(Morph)}:{Morph}";

        public override int GetHashCode() => Text.GetHashCode() ^ Morph.GetHashCode();
        public override bool Equals(object obj) => Equals(obj as SignBlockData);
        public override bool Equals(ExtraBlockData obj) => Equals(obj as SignBlockData);
        public bool Equals(SignBlockData o)
        {
            if (o == null) return false;
            return Morph == o.Morph && Text == o.Text;
        }
    }
    public class PortalBlockData : ExtraBlockData, IEquatable<PortalBlockData>, IMorphable, IFlippable
    {
        public PortalBlockData(List<object> data, ref int index) => Deserialize(data, ref index);
        public PortalBlockData(int rotation = 0, int thisId = 0, int targetId = 0, bool flipped = false)
        {
            this.Rotation = rotation;
            this.ThisId = thisId;
            this.TargetId = targetId;
            this.Flipped = flipped;
        }

        public int Rotation { get; set; }
        int IMorphable.Morph { get => Rotation; set => Rotation = value; }
        public int ThisId { get; set; }
        public int TargetId { get; set; }
        public bool Flipped { get; set; }
        bool IFlippable.Flipped { get => Flipped; set => Flipped = value; }

        public override List<object> Serialize() => new List<object>() { Rotation, ThisId, TargetId, Flipped };
        public static PortalBlockData Deserialize(List<object> data, ref int index) => new PortalBlockData((int)data[index++], (int)data[index++], (int)data[index++], (bool)data[index++]);

        public override string ToString() => $"{nameof(Rotation)}:{Rotation}, {nameof(ThisId)}:{ThisId}, {nameof(TargetId)}:{TargetId}, {nameof(Flipped)}:{Flipped}";

        public override int GetHashCode() => Rotation ^ ThisId ^ TargetId ^ Flipped.GetHashCode();
        public override bool Equals(object obj) => Equals(obj as PortalBlockData);
        public override bool Equals(ExtraBlockData obj) => Equals(obj as PortalBlockData);
        public bool Equals(PortalBlockData o)
        {
            if (o == null) return false;
            return Flipped == o.Flipped && Rotation == o.Rotation && ThisId == o.ThisId && TargetId == o.TargetId;
        }
    }
    public class EffectBlockData : ExtraBlockData, IEquatable<EffectBlockData>, IMorphable
    {
        public EffectBlockData(List<object> data, ref int index) => Deserialize(data, ref index);
        public EffectBlockData(int value = 0) => this.Value = value;

        public int Value { get; set; }
        int IMorphable.Morph { get => Value; set => Value = value; }

        public override List<object> Serialize() => new List<object>() { Value };
        public static EffectBlockData Deserialize(List<object> data, ref int index) => new EffectBlockData((int)data[index++]);

        public override string ToString() => $"{nameof(Value)}:{Value}";

        public override int GetHashCode() => Value;
        public override bool Equals(object obj) => Equals(obj as EffectBlockData);
        public override bool Equals(ExtraBlockData obj) => Equals(obj as EffectBlockData);
        public bool Equals(EffectBlockData o)
        {
            if (o == null) return false;
            return Value == o.Value;
        }
    }
    public class SwitchBlockData : ExtraBlockData, IEquatable<SwitchBlockData>, IMorphable, IFlippable
    {
        public SwitchBlockData(List<object> data, ref int index, bool isDoor) => Deserialize(data, ref index, isDoor);
        public SwitchBlockData(int value = 0, bool? inverted = null)
        {
            this.Value = value;
            this.Inverted = inverted;
        }

        public int Value { get; set; }
        int IMorphable.Morph { get => Value; set => Value = value; }
        /// <summary>
        /// null for non-doors
        /// </summary>
        public bool? Inverted { get; set; }
        bool IFlippable.Flipped { get => Inverted ?? throw new NullReferenceException($"{nameof(Inverted)} is null."); set => Inverted = value; }

        public override List<object> Serialize() => Inverted.HasValue ? new List<object>() { Value, Inverted.Value } : new List<object>() { Value };
        public static SwitchBlockData Deserialize(List<object> data, ref int index, bool isDoor) => new SwitchBlockData((int)data[index++], isDoor ? (bool)data[index++] : null as bool?);

        public override string ToString() => $"{nameof(Value)}:{Value}{(Inverted.HasValue ? $", {nameof(Inverted)}:{Inverted}" : "")}";

        public override int GetHashCode() => Value ^ Inverted.GetHashCode();
        public override bool Equals(object obj) => Equals(obj as SwitchBlockData);
        public override bool Equals(ExtraBlockData obj) => Equals(obj as SwitchBlockData);
        public bool Equals(SwitchBlockData o)
        {
            if (o == null) return false;
            return Value == o.Value && Inverted == o.Inverted;
        }
    }
    public class PlatformBlockData : ExtraBlockData, IEquatable<PlatformBlockData>, IMorphable
    {
        public PlatformBlockData(List<object> data, ref int index) => Deserialize(data, ref index);
        public PlatformBlockData(int rotation = 0) => this.Rotation = rotation;

        public int Rotation { get; set; }
        int IMorphable.Morph { get => Rotation; set => Rotation = value; }

        public override List<object> Serialize() => new List<object>() { Rotation };
        public static PlatformBlockData Deserialize(List<object> data, ref int index) => new PlatformBlockData((int)data[index++]);

        public override string ToString() => $"{nameof(Rotation)}:{Rotation}";

        public override int GetHashCode() => Rotation;
        public override bool Equals(object obj) => Equals(obj as PlatformBlockData);
        public override bool Equals(ExtraBlockData obj) => Equals(obj as PlatformBlockData);
        public bool Equals(PlatformBlockData o)
        {
            if (o == null) return false;
            return Rotation == o.Rotation;
        }
    }
}
