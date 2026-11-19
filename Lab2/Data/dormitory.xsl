<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" encoding="utf-8" indent="yes"/>
  <xsl:template match="/">
    <html>
      <head>
        <meta charset="utf-8"/>
        <title>Звіт: Мешканці гуртожитку</title>
        <style>
          body { font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif; margin: 30px; background: #f8f9fa; }
          h1 { color: #1e3a8a; }
          table { border-collapse: collapse; width: 100%; background: #fff; box-shadow: 0 2px 8px rgba(0,0,0,0.08); }
          th, td { border: 1px solid #cbd5e1; padding: 10px 14px; text-align: left; }
          th { background-color: #1e40af; color: white; }
          tr:nth-child(even) { background-color: #f1f5f9; }
        </style>
      </head>
      <body>
        <h1><xsl:value-of select="Dormitory/@Name"/></h1>
        <table>
          <tr>
            <th>П.І.П.</th>
            <th>Факультет</th>
            <th>Кафедра</th>
            <th>Курс</th>
            <th>Пільга</th>
            <th>Телефон</th>
            <th>Корпус / Кімната</th>
            <th>Період проживання</th>
          </tr>
          <xsl:for-each select="Dormitory/Resident">
            <tr>
              <td><xsl:value-of select="@FullName"/></td>
              <td><xsl:value-of select="@Faculty"/></td>
              <td><xsl:value-of select="@Department"/></td>
              <td><xsl:value-of select="@Course"/></td>
              <td>
                <xsl:choose>
                  <xsl:when test="@Benefit"><xsl:value-of select="@Benefit"/></xsl:when>
                  <xsl:otherwise>—</xsl:otherwise>
                </xsl:choose>
              </td>
              <td>
                <xsl:choose>
                  <xsl:when test="@Phone"><xsl:value-of select="@Phone"/></xsl:when>
                  <xsl:otherwise>—</xsl:otherwise>
                </xsl:choose>
              </td>
              <td>
                <xsl:value-of select="Accommodation/@Building"/>, кімн. <xsl:value-of select="Accommodation/@Room"/>
              </td>
              <td>
                <xsl:value-of select="Accommodation/@CheckInDate"/> — <xsl:value-of select="Accommodation/@CheckOutDate"/>
              </td>
            </tr>
          </xsl:for-each>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
