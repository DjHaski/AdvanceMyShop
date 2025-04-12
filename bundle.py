import os
from pathlib import Path
import zipfile

MOD_NAME = 'AdvanceMyShop'
RESULTING_ZIP_NAME = Path(f'Release/{MOD_NAME}-Release.zip')

exc = os.system('dotnet build')
if exc != 0:
    raise Exception('dotnet build failed')

RESULTING_ZIP_NAME.parent.mkdir(parents=True, exist_ok=True)
if RESULTING_ZIP_NAME.exists():
    RESULTING_ZIP_NAME.unlink()

with zipfile.ZipFile(RESULTING_ZIP_NAME, 'w', zipfile.ZIP_DEFLATED) as zip:
    zip.write(f'bin/Debug/netstandard2.1/{MOD_NAME}.dll', arcname=f'{MOD_NAME}.dll')
    zip.write('manifest.json')
    zip.write('icon.png')
    zip.write('LICENSE')
    zip.write('README.md')
    zip.write('CHANGELOG.md')

print(f'Created {RESULTING_ZIP_NAME}, build was successful!')