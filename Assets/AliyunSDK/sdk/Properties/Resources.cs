using System;
using System.Collections.Generic;
using System.Text;

namespace Aliyun.OSS.Properties
{
    internal class Resources
    {
        internal static readonly string ExceptionEndOperationHasBeenCalled = "以前使用 asyncResult调用过此方法。";
        internal static readonly string ExceptionIfArgumentStringIsNullOrEmpty = "参数为空引用或值为长度是零的字符串。";
        internal static readonly string ExceptionInvalidResponse = "返回结果无法解析。";
        internal static readonly string ExceptionUnknownError = "服务器返回未知错误。";

        internal static readonly string MimeData =
            "xlsx    application/vnd.openxmlformats-officedocument.spreadsheetml.sheet\n" +
            "xltx    application/vnd.openxmlformats-officedocument.spreadsheetml.template\n" +
            "potx    application/vnd.openxmlformats-officedocument.presentationml.template\n" +
            "ppsx    application/vnd.openxmlformats-officedocument.presentationml.slideshow\n" +
            "pptx    application/vnd.openxmlformats-officedocument.presentationml.presentation\n" +
            "sldx    application/vnd.openxmlformats-officedocument.presentationml.slide\n" +
            "docx    application/vnd.openxmlformats-officedocument.wordprocessingml.document\n" +
            "dotx    application/vnd.openxmlformats-officedocument.wordprocessingml.template\n" +
            "xlam    application/vnd.ms-excel.addin.macroEnabled.12\n" +
            "xlsb    application/vnd.ms-excel.sheet.binary.macroEnabled.12\n" +
            "apk\t\tapplication/vnd.android.package-archive\n" +
            "hqx\t\tapplication/mac-binhex40\n" +
            "cpt\t\tapplication/mac-compactpro\n" +
            "doc\t\tapplication/msword\n" +
            "ogg\t\tapplication/ogg\n" +
            "pdf\t\tapplication/pdf\n" +
            "rtf\t\ttext/rtf\n" +
            "mif\t\tapplication/vnd.mif\n" +
            "xls\t\tapplication/vnd.ms-excel\n" +
            "ppt\t\tapplication/vnd.ms-powerpoint\n" +
            "odc\t\tapplication/vnd.oasis.opendocument.chart\n" +
            "odb\t\tapplication/vnd.oasis.opendocument.database\n" +
            "odf\t\tapplication/vnd.oasis.opendocument.formula\n" +
            "odg\t\tapplication/vnd.oasis.opendocument.graphics\n" +
            "otg\t\tapplication/vnd.oasis.opendocument.graphics-template\n" +
            "odi\t\tapplication/vnd.oasis.opendocument.image\n" +
            "odp\t\tapplication/vnd.oasis.opendocument.presentation\n" +
            "otp\t\tapplication/vnd.oasis.opendocument.presentation-template\n" +
            "ods\t\tapplication/vnd.oasis.opendocument.spreadsheet\n" +
            "ots\t\tapplication/vnd.oasis.opendocument.spreadsheet-template\n" +
            "odt\t\tapplication/vnd.oasis.opendocument.text\n" +
            "odm\t\tapplication/vnd.oasis.opendocument.text-master\n" +
            "ott\t\tapplication/vnd.oasis.opendocument.text-template\n" +
            "oth\t\tapplication/vnd.oasis.opendocument.text-web\n" +
            "sxw\t\tapplication/vnd.sun.xml.writer\n" +
            "stw\t\tapplication/vnd.sun.xml.writer.template\n" +
            "sxc\t\tapplication/vnd.sun.xml.calc\n" +
            "stc\t\tapplication/vnd.sun.xml.calc.template\n" +
            "sxd\t\tapplication/vnd.sun.xml.draw\n" +
            "std\t\tapplication/vnd.sun.xml.draw.template\n" +
            "sxi\t\tapplication/vnd.sun.xml.impress\n" +
            "sti\t\tapplication/vnd.sun.xml.impress.template\n" +
            "sxg\t\tapplication/vnd.sun.xml.writer.global\n" +
            "sxm\t\tapplication/vnd.sun.xml.math\n" +
            "sis\t\tapplication/vnd.symbian.install\n" +
            "wbxml\tapplication/vnd.wap.wbxml\n" +
            "wmlc\tapplication/vnd.wap.wmlc\n" +
            "wmlsc\tapplication/vnd.wap.wmlscriptc\n" +
            "bcpio\tapplication/x-bcpio\n" +
            "torrent\tapplication/x-bittorrent\n" +
            "bz2\t\tapplication/x-bzip2\n" +
            "vcd\t\tapplication/x-cdlink\n" +
            "pgn\t\tapplication/x-chess-pgn\n" +
            "cpio    application/x-cpio\n" +
            "csh\t\tapplication/x-csh\n" +
            "dvi\t\tapplication/x-dvi\n" +
            "spl\t\tapplication/x-futuresplash\n" +
            "gtar    application/x-gtar\n" +
            "hdf\t\tapplication/x-hdf\n" +
            "jar\t\tapplication/x-java-archive\n" +
            "jnlp    application/x-java-jnlp-file\n" +
            "js\t\tapplication/x-javascript\n" +
            "ksp\t\tapplication/x-kspread\n" +
            "chrt    application/x-kchart\n" +
            "kil\t\tapplication/x-killustrator\n" +
            "latex\tapplication/x-latex\n" +
            "rpm\t\tapplication/x-rpm\n" +
            "sh\t\tapplication/x-sh\n" +
            "shar    application/x-shar\n" +
            "swf\t\tapplication/x-shockwave-flash\n" +
            "sit\t\tapplication/x-stuffit\n" +
            "sv4cpio\tapplication/x-sv4cpio\n" +
            "sv4crc\tapplication/x-sv4crc\n" +
            "tar\t\tapplication/x-tar\n" +
            "tcl\t\tapplication/x-tcl\n" +
            "tex\t\tapplication/x-tex\n" +
            "man\t\tapplication/x-troff-man\n" +
            "me\t\tapplication/x-troff-me\n" +
            "ms\t\tapplication/x-troff-ms\n" +
            "ustar\tapplication/x-ustar\n" +
            "src\t\tapplication/x-wais-source\n" +
            "zip\t\tapplication/zip\n" +
            "m3u\t\taudio/x-mpegurl\n" +
            "ra\t\taudio/x-pn-realaudio\n" +
            "wav\t\taudio/x-wav\n" +
            "wma\t\taudio/x-ms-wma\n" +
            "wax\t\taudio/x-ms-wax\n" +
            "pdb\t\tchemical/x-pdb\n" +
            "xyz\t    chemical/x-xyz\n" +
            "bmp\t    image/bmp\n" +
            "gi\t\timage/gif\n" +
            "ief\t\timage/ief\n" +
            "png\t    image/png\n" +
            "wbmp    image/vnd.wap.wbmp\n" +
            "ras\t    image/x-cmu-raster\n" +
            "pnm\t    image/x-portable-anymap\n" +
            "pbm\t    image/x-portable-bitmap\n" +
            "pgm\t    image/x-portable-graymap\n" +
            "ppm\t    image/x-portable-pixmap\n" +
            "rgb\t    image/x-rgb\n" +
            "xbm\t    image/x-xbitmap\n" +
            "xpm\t\timage/x-xpixmap\n" +
            "xwd\t    image/x-xwindowdump\n" +
            "css\t    text/css\n" +
            "rtx\t    text/richtext\n" +
            "tsv\t    text/tab-separated-values\n" +
            "jad\t    text/vnd.sun.j2me.app-descriptor\n" +
            "wml\t    text/vnd.wap.wml\n" +
            "wmls\ttext/vnd.wap.wmlscript\n" +
            "etx\t    text/x-setext\n" +
            "mxu\t    video/vnd.mpegurl\n" +
            "flv\t    video/x-flv\n" +
            "wm\t    video/x-ms-wm\n" +
            "wmv\t    video/x-ms-wmv\n" +
            "wmx\t    video/x-ms-wmx\n" +
            "wvx\t    video/x-ms-wvx\n" +
            "avi\t    video/x-msvideo\n" +
            "movie\tvideo/x-sgi-movie\n" +
            "ice\t\tx-conference/x-cooltalk\n" +
            "3gp\t    video/3gpp\n" +
            "ai\t    application/postscript\n" +
            "aif\t    audio/x-aiff\n" +
            "aifc    audio/x-aiff\n" +
            "aiff    audio/x-aiff\n" +
            "asc\t    text/plain\n" +
            "atom    application/atom+xml\n" +
            "au\t    audio/basic\n" +
            "bin\t\tapplication/octet-stream\n" +
            "cdf     application/x-netcdf\n" +
            "cgm     image/cgm\n" +
            "class   application/octet-stream\n" +
            "dcr     application/x-director\n" +
            "dif     video/x-dv\n" +
            "dir     application/x-director\n" +
            "djv     image/vnd.djvu\n" +
            "djvu    image/vnd.djvu\n" +
            "dll     application/octet-stream\n" +
            "dmg     application/octet-stream\n" +
            "dms     application/octet-stream\n" +
            "dtd     application/xml-dtd\n" +
            "dv      video/x-dv\n" +
            "dxr     application/x-director\n" +
            "eps     application/postscript\n" +
            "exe     application/octet-stream\n" +
            "ez      application/andrew-inset\n" +
            "gram    application/srgs\n" +
            "grxml   application/srgs+xml\n" +
            "gz      application/x-gzip\n" +
            "htm     text/html\n" +
            "html    text/html\n" +
            "ico     image/x-icon\n" +
            "ics     text/calendar\n" +
            "ifb     text/calendar\n" +
            "iges    model/iges\n" +
            "igs     model/iges\n" +
            "jp2     image/jp2\n" +
            "jpe     image/jpeg\n" +
            "jpeg    image/jpeg\n" +
            "jpg     image/jpeg\n" +
            "kar     audio/midi\n" +
            "lha     application/octet-stream\n" +
            "lzh     application/octet-stream\n" +
            "m4a     audio/mp4a-latm\n" +
            "m4p     audio/mp4a-latm\n" +
            "m4u     video/vnd.mpegurl\n" +
            "m4v     video/x-m4v\n" +
            "mac     image/x-macpaint\n" +
            "mathml  application/mathml+xml\n" +
            "mesh    model/mesh\n" +
            "mid     audio/midi\n" +
            "midi    audio/midi\n" +
            "mov     video/quicktime\n" +
            "mp2     audio/mpeg\n" +
            "mp3     audio/mpeg\n" +
            "mp4     video/mp4\n" +
            "mpe     video/mpeg\n" +
            "mpeg    video/mpeg\n" +
            "mpg     video/mpeg\n" +
            "mpga    audio/mpeg\n" +
            "msh     model/mesh\n" +
            "nc      application/x-netcdf\n" +
            "oda     application/oda\n" +
            "ogv     video/ogv\n" +
            "pct     image/pict\n" +
            "pic     image/pict\n" +
            "pict    image/pict\n" +
            "pnt     image/x-macpaint\n" +
            "pntg    image/x-macpaint\n" +
            "ps      application/postscript\n" +
            "qt      video/quicktime\n" +
            "qti     image/x-quicktime\n" +
            "qtif    image/x-quicktime\n" +
            "ram     audio/x-pn-realaudio\n" +
            "rdf     application/rdf+xml\n" +
            "rm      application/vnd.rn-realmedia\n" +
            "roff    application/x-troff\n" +
            "sgm     text/sgml\n" +
            "sgml    text/sgml\n" +
            "silo    model/mesh\n" +
            "skd     application/x-koan\n" +
            "skm     application/x-koan\n" +
            "skp     application/x-koan\n" +
            "skt     application/x-koan\n" +
            "smi     application/smil\n" +
            "smil    application/smil\n" +
            "snd     audio/basic\n" +
            "so      application/octet-stream\n" +
            "svg     image/svg+xml\n" +
            "t       application/x-troff\n" +
            "texi    application/x-texinfo\n" +
            "texinfo pplication/x-texinfo\n" +
            "tif     image/tiff\n" +
            "tiff    image/tiff\n" +
            "tr      application/x-troff\n" +
            "txt     text/plain\n" +
            "vrml    model/vrml\n" +
            "vxml    application/voicexml+xml\n" +
            "webm    video/webm\n" +
            "wrl     model/vrml\n" +
            "xht     application/xhtml+xml\n" +
            "xhtml   application/xhtml+xml\n" +
            "xml     application/xml\n" +
            "xsl     application/xml\n" +
            "xslt    application/xslt+xml\n" +
            "xul     application/vnd.mozilla.xul+xml\n" +
            "webp    image/webp\n";
    }
}
